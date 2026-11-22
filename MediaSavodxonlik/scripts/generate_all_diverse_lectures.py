# -*- coding: utf-8 -*-
"""
Generate all 20 extensive, beautifully styled DOCX lecture documents
Meeting all user requirements:
1. Title starts simply as: "Mavzu: <Mavzu nomi>"
2. Exactly 3 plans (reja) explicitly listed at the top
3. Diverse diagram layouts: Flow, Grid, Versus, Stack, Inspector (not all 3-card identical!)
4. Detailed, extensive, academic lecture content
"""

import os
import docx
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH

from visual_engine import (
    draw_process_flow,
    draw_grid_matrix,
    draw_versus_columns,
    draw_layered_stack,
    draw_checklist_inspector
)
from more_rich_topics import ALL_20_RICH_TOPICS

CONTENT_DIR = os.path.abspath("Content")
TEMP_IMG_DIR = os.path.abspath("temp_diverse_diagrams")
os.makedirs(TEMP_IMG_DIR, exist_ok=True)

def generate_diagram(topic, img_path):
    dtype = topic["diagram_type"]
    ddata = topic["diagram_data"]
    title = ddata["title"]
    subtitle = ddata["subtitle"]

    if dtype == "flow":
        draw_process_flow(title, subtitle, ddata["steps"], img_path)
    elif dtype == "grid":
        draw_grid_matrix(title, subtitle, ddata["quadrants"], img_path)
    elif dtype == "versus":
        draw_versus_columns(title, subtitle, ddata["left"], ddata["right"], img_path)
    elif dtype == "stack":
        draw_layered_stack(title, subtitle, ddata["layers"], img_path)
    elif dtype == "inspector":
        draw_checklist_inspector(title, subtitle, ddata["items"], img_path)
    else:
        # Default to flow
        draw_process_flow(title, subtitle, ddata.get("steps", []), img_path)

def create_rich_docx(topic):
    num = topic["num"]
    title = topic["title"]
    plans = topic["plans"]
    intro = topic["intro"]
    plan_contents = topic["plan_contents"]
    concl = topic["conclusion"]
    questions = topic["questions"]

    folder = os.path.join(CONTENT_DIR, f"topic-{num:02d}", "lecture")
    os.makedirs(folder, exist_ok=True)

    img_path = os.path.join(TEMP_IMG_DIR, f"diagram_{num:02d}.png")
    generate_diagram(topic, img_path)

    doc = docx.Document()

    # Set document margins (normal 1 inch)
    for section in doc.sections:
        section.top_margin = Inches(1)
        section.bottom_margin = Inches(1)
        section.left_margin = Inches(1)
        section.right_margin = Inches(1)

    # 1. Title Requirement: Shunchaki "Mavzu: <Mavzu nomi>"
    p_title = doc.add_paragraph()
    p_title.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_title.paragraph_format.space_before = Pt(0)
    p_title.paragraph_format.space_after = Pt(16)
    
    run_title = p_title.add_run(f"Mavzu: {title}")
    run_title.font.name = "Segoe UI"
    run_title.font.size = Pt(21)
    run_title.font.bold = True
    run_title.font.color.rgb = RGBColor(30, 41, 59) # Slate 800

    # 2. Plan Requirement: Exactly 3 plans clearly numbered
    p_plan_head = doc.add_paragraph()
    p_plan_head.paragraph_format.space_before = Pt(8)
    p_plan_head.paragraph_format.space_after = Pt(6)
    run_plan_lbl = p_plan_head.add_run("REJA:")
    run_plan_lbl.font.name = "Segoe UI"
    run_plan_lbl.font.size = Pt(13)
    run_plan_lbl.font.bold = True
    run_plan_lbl.font.color.rgb = RGBColor(13, 148, 136) # Teal 600

    for idx, pl in enumerate(plans, start=1):
        p_pl = doc.add_paragraph()
        p_pl.paragraph_format.left_indent = Inches(0.3)
        p_pl.paragraph_format.space_after = Pt(4)
        run_pl_num = p_pl.add_run(f"{idx}. ")
        run_pl_num.font.name = "Segoe UI"
        run_pl_num.font.bold = True
        run_pl_num.font.color.rgb = RGBColor(14, 165, 233) # Sky 500
        run_pl_txt = p_pl.add_run(pl)
        run_pl_txt.font.name = "Segoe UI"
        run_pl_txt.font.size = Pt(11.5)

    # Kirish / Dolzarbligi
    p_intro = doc.add_paragraph()
    p_intro.paragraph_format.space_before = Pt(14)
    p_intro.paragraph_format.space_after = Pt(16)
    p_intro.paragraph_format.line_spacing = 1.25
    run_in_lbl = p_intro.add_run("Kirish va mavzuning dolzarbligi: ")
    run_in_lbl.font.name = "Segoe UI"
    run_in_lbl.font.bold = True
    run_in_lbl.font.color.rgb = RGBColor(15, 118, 110)
    run_in_txt = p_intro.add_run(intro)
    run_in_txt.font.name = "Segoe UI"
    run_in_txt.font.size = Pt(11.5)
    run_in_txt.font.italic = True

    # 1-Reja bayoni
    doc.add_heading(f"1-Reja. {plans[0]}", level=2)
    for para in plan_contents[0]:
        p = doc.add_paragraph(para)
        p.paragraph_format.line_spacing = 1.25
        p.paragraph_format.space_after = Pt(9)
        p.runs[0].font.name = "Segoe UI"
        p.runs[0].font.size = Pt(11.5)

    # Rasm: Didaktik ko'rgazmali diagramma (mavzu xususiyatiga ko'ra turli tuman)
    p_img = doc.add_paragraph()
    p_img.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_img.paragraph_format.space_before = Pt(14)
    p_img.paragraph_format.space_after = Pt(6)
    r_img = p_img.add_run()
    r_img.add_picture(img_path, width=Inches(6.2))

    p_cap = doc.add_paragraph()
    p_cap.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_cap.paragraph_format.space_after = Pt(18)
    run_cap = p_cap.add_run(f"{num}-rasm. {topic['diagram_data']['title']} (Didaktik ko‘rgazmali material)")
    run_cap.font.name = "Segoe UI"
    run_cap.font.size = Pt(10)
    run_cap.font.italic = True
    run_cap.font.color.rgb = RGBColor(100, 116, 139)

    # 2-Reja bayoni
    doc.add_heading(f"2-Reja. {plans[1]}", level=2)
    for para in plan_contents[1]:
        p = doc.add_paragraph(para)
        p.paragraph_format.line_spacing = 1.25
        p.paragraph_format.space_after = Pt(9)
        p.runs[0].font.name = "Segoe UI"
        p.runs[0].font.size = Pt(11.5)

    # 3-Reja bayoni
    doc.add_heading(f"3-Reja. {plans[2]}", level=2)
    for para in plan_contents[2]:
        p = doc.add_paragraph(para)
        p.paragraph_format.line_spacing = 1.25
        p.paragraph_format.space_after = Pt(9)
        p.runs[0].font.name = "Segoe UI"
        p.runs[0].font.size = Pt(11.5)

    # Xulosa
    doc.add_heading("Xulosa va amaliy ko‘nikmalar", level=3)
    p_con = doc.add_paragraph(concl)
    p_con.paragraph_format.line_spacing = 1.25
    p_con.paragraph_format.space_after = Pt(14)
    p_con.runs[0].font.name = "Segoe UI"
    p_con.runs[0].font.size = Pt(11.5)

    # Nazorat savollari
    doc.add_heading("Nazorat va o‘z-o‘zini tekshirish savollari", level=3)
    for q_idx, q in enumerate(questions, start=1):
        p_q = doc.add_paragraph()
        p_q.paragraph_format.space_after = Pt(5)
        run_qn = p_q.add_run(f"{q_idx}. ")
        run_qn.font.name = "Segoe UI"
        run_qn.font.bold = True
        run_qn.font.color.rgb = RGBColor(14, 165, 233)
        run_qt = p_q.add_run(q)
        run_qt.font.name = "Segoe UI"
        run_qt.font.size = Pt(11)

    out_file = os.path.join(folder, f"Ma'ruza matni - {num:02d}-mavzu.docx")
    doc.save(out_file)
    print(f"Generated: {out_file} (Layout: {topic['diagram_type']})")

def main():
    print(f"Building {len(ALL_20_RICH_TOPICS)} high-quality lectures with diverse diagrams...")
    for t in ALL_20_RICH_TOPICS:
        create_rich_docx(t)
    print("ALL 20 LECTURES SUCCESSFULLY GENERATED WITH DIVERSE GRAPHICS AND DETAILED CONTENT!")

if __name__ == "__main__":
    main()

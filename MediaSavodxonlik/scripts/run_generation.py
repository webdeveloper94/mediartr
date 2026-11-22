# -*- coding: utf-8 -*-
"""
Generate 20 professional DOCX lecture files with custom infographic images.
"""

import os
import sys
import docx
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.enum.table import WD_TABLE_ALIGNMENT
from docx.oxml import OxmlElement, parse_xml
from docx.oxml.ns import nsdecls, qn

from generate_lectures import create_infographic
from topics_dataset import ALL_TOPICS

CONTENT_DIR = os.path.abspath("Content")
TEMP_IMG_DIR = os.path.abspath("temp_infographics")
os.makedirs(TEMP_IMG_DIR, exist_ok=True)

def build_docx(topic):
    num = topic["num"]
    title = topic["title"]
    folder = os.path.join(CONTENT_DIR, f"topic-{num:02d}", "lecture")
    os.makedirs(folder, exist_ok=True)

    img_filename = os.path.join(TEMP_IMG_DIR, f"diagram_{num:02d}.png")
    diag_data = topic["diagram"]
    create_infographic(diag_data["title"], diag_data["subtitle"], diag_data["blocks"], img_filename)

    doc = docx.Document()

    # Style definitions
    # Title
    p_title = doc.add_paragraph()
    p_title.alignment = WD_ALIGN_PARAGRAPH.CENTER
    run_badge = p_title.add_run(f"MEDIA VA AXBOROT SAVODXONLIGI · {num:02d}-MAVZU\n")
    run_badge.font.size = Pt(11)
    run_badge.font.bold = True
    run_badge.font.color.rgb = RGBColor(14, 165, 233) # Sky blue

    run_title = p_title.add_run(title)
    run_title.font.size = Pt(22)
    run_title.font.bold = True
    run_title.font.color.rgb = RGBColor(15, 23, 42) # Slate 900

    # Intro box / Callout
    p_intro = doc.add_paragraph()
    p_intro.paragraph_format.space_before = Pt(12)
    p_intro.paragraph_format.space_after = Pt(18)
    run_intro_lbl = p_intro.add_run("Mavzuning dolzarbligi va maqsadi: ")
    run_intro_lbl.font.bold = True
    run_intro_lbl.font.size = Pt(12)
    run_intro_lbl.font.color.rgb = RGBColor(13, 148, 136) # Teal
    run_intro = p_intro.add_run(topic["intro"])
    run_intro.font.size = Pt(11.5)
    run_intro.font.italic = True

    # Section 1
    sec1 = topic["sections"][0]
    p_h1 = doc.add_heading(sec1["title"], level=2)
    p_h1.paragraph_format.space_before = Pt(14)
    for p_text in sec1["paragraphs"]:
        p = doc.add_paragraph(p_text)
        p.paragraph_format.line_spacing = 1.2
        p.paragraph_format.space_after = Pt(8)

    # Insert Infographic Image here!
    p_img = doc.add_paragraph()
    p_img.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_img.paragraph_format.space_before = Pt(14)
    p_img.paragraph_format.space_after = Pt(6)
    run_img = p_img.add_run()
    run_img.add_picture(img_filename, width=Inches(6.2))

    p_cap = doc.add_paragraph()
    p_cap.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_cap.paragraph_format.space_after = Pt(18)
    run_cap = p_cap.add_run(f"{num}-rasm. {diag_data['title']} (Didaktik ko‘rgazmali sxema)")
    run_cap.font.size = Pt(10)
    run_cap.font.italic = True
    run_cap.font.color.rgb = RGBColor(100, 116, 139)

    # Remaining sections
    for sec in topic["sections"][1:]:
        p_h = doc.add_heading(sec["title"], level=2)
        p_h.paragraph_format.space_before = Pt(14)
        for p_text in sec["paragraphs"]:
            p = doc.add_paragraph(p_text)
            p.paragraph_format.line_spacing = 1.2
            p.paragraph_format.space_after = Pt(8)

    # Conclusion
    doc.add_heading("Xulosa va amaliy tavsiyalar", level=3)
    p_concl = doc.add_paragraph(topic["conclusion"])
    p_concl.paragraph_format.line_spacing = 1.2
    p_concl.paragraph_format.space_after = Pt(14)

    # Self-assessment questions
    doc.add_heading("Nazorat va o‘z-o‘zini tekshirish savollari", level=3)
    for q_idx, q in enumerate(topic["questions"], start=1):
        p_q = doc.add_paragraph()
        run_q_num = p_q.add_run(f"{q_idx}. ")
        run_q_num.font.bold = True
        run_q_num.font.color.rgb = RGBColor(14, 165, 233)
        run_q_txt = p_q.add_run(q)
        run_q_txt.font.size = Pt(11)
        p_q.paragraph_format.space_after = Pt(4)

    out_path = os.path.join(folder, f"Ma'ruza matni - {num:02d}-mavzu.docx")
    doc.save(out_path)
    print(f"Generated: {out_path}")

def main():
    print(f"Generating lectures for {len(ALL_TOPICS)} topics...")
    for t in ALL_TOPICS:
        build_docx(t)
    print("ALL 20 LECTURES GENERATED SUCCESSFULLY!")

if __name__ == "__main__":
    main()

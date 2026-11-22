# -*- coding: utf-8 -*-
"""
Generate comprehensive, full-scale academic lectures (1500+ words each) for all 20 topics.
Includes:
- "Mavzu: <Mavzu nomi>"
- Explicit "REJA:" with 3 comprehensive analytical sub-points
- Extended "Kirish va mavzuning dolzarbligi" (2-3 deep paragraphs)
- Extensive Plan 1, Plan 2, Plan 3 (each with 5-7 detailed academic paragraphs, real-world case studies, legislation, methodologies, international practices)
- Embedded high-res didactic diagrams tailored to each topic
- Deep "Xulosa va amaliy tavsiyalar"
- "Nazorat va muhokama savollari" (5 comprehensive reflection questions)
- "Tavsiya etiladigan adabiyotlar va manbalar" (citing Taylaqov, UNESCO, Alimov, Lex.uz, Factcheck.uz)
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

CONTENT_DIR = os.path.abspath("Content")
TEMP_IMG_DIR = os.path.abspath("temp_extended_diagrams")
os.makedirs(TEMP_IMG_DIR, exist_ok=True)

# Helper to format rich docx
def setup_document_styles():
    doc = docx.Document()
    for section in doc.sections:
        section.top_margin = Inches(0.9)
        section.bottom_margin = Inches(0.9)
        section.left_margin = Inches(1.0)
        section.right_margin = Inches(1.0)
    return doc

print("Academic generator helper loaded.")

# -*- coding: utf-8 -*-
"""
Generate 20 comprehensive, beautifully styled lecture documents (DOCX)
with pedagogical infographic diagrams for the MediaSavodxonlik course.
Based strictly on the official Modul Namonaviy O'quv Dasturi.
"""

import os
import docx
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.enum.table import WD_TABLE_ALIGNMENT
from docx.oxml import OxmlElement, parse_xml
from docx.oxml.ns import nsdecls, qn
from PIL import Image, ImageDraw, ImageFont

# Set up output directories
CONTENT_DIR = os.path.abspath("Content")

def create_infographic(title, subtitle, blocks, filename):
    width, height = 960, 480
    im = Image.new("RGB", (width, height), color="#0F172A")
    draw = ImageDraw.Draw(im)

    # Gradient background simulation / frames
    draw.rectangle([12, 12, width - 12, height - 12], outline="#334155", width=2)
    draw.rectangle([16, 16, width - 16, 85], fill="#1E293B")
    
    # Header line
    draw.rectangle([16, 83, width - 16, 86], fill="#2DD4BF")

    # Header text
    try:
        # Fallback to default font or truetype if available
        font_title = ImageFont.truetype("arial.ttf", 24)
        font_sub = ImageFont.truetype("arial.ttf", 15)
        font_card_h = ImageFont.truetype("arial.ttf", 17)
        font_card_b = ImageFont.truetype("arial.ttf", 13)
        font_tag = ImageFont.truetype("arial.ttf", 11)
    except:
        font_title = font_sub = font_card_h = font_card_b = font_tag = ImageFont.load_default()

    draw.text((36, 26), title, fill="#F8FAFC", font=font_title)
    draw.text((36, 56), subtitle, fill="#94A3B8", font=font_sub)

    # Draw blocks (up to 3 or 4 blocks)
    n = len(blocks)
    card_margin = 20
    available_w = width - 40 - (n - 1) * card_margin
    card_w = available_w // n
    card_top = 110
    card_h = 330

    card_colors = [
        {"bg": "#1E293B", "border": "#2DD4BF", "accent": "#2DD4BF", "badge": "#0F766E"},
        {"bg": "#1E293B", "border": "#38BDF8", "accent": "#38BDF8", "badge": "#0369A1"},
        {"bg": "#1E293B", "border": "#F472B6", "accent": "#F472B6", "badge": "#9D174D"},
        {"bg": "#1E293B", "border": "#FBBF24", "accent": "#FBBF24", "badge": "#92400E"}
    ]

    for i, b in enumerate(blocks):
        x0 = 20 + i * (card_w + card_margin)
        x1 = x0 + card_w
        c = card_colors[i % len(card_colors)]

        # Card base
        draw.rectangle([x0, card_top, x1, card_top + card_h], fill=c["bg"], outline=c["border"], width=2)
        
        # Step / Tag Badge
        badge_text = f"BOSQICH 0{i+1}" if "step" in b else b.get("badge", f"QISM 0{i+1}")
        draw.rectangle([x0 + 16, card_top + 16, x0 + 110, card_top + 38], fill=c["badge"])
        draw.text((x0 + 24, card_top + 20), badge_text, fill="#FFFFFF", font=font_tag)

        # Card Header
        head_txt = b.get("title", "")
        draw.text((x0 + 16, card_top + 48), head_txt, fill=c["accent"], font=font_card_h)

        # Separator line
        draw.line([x0 + 16, card_top + 78, x1 - 16, card_top + 78], fill="#334155", width=1)

        # Card Body lines (wrap text manually)
        body_text = b.get("text", "")
        words = body_text.split()
        lines = []
        cur_line = []
        for w in words:
            cur_line.append(w)
            # Rough character limit per card width
            if len(" ".join(cur_line)) > (card_w // 10):
                lines.append(" ".join(cur_line[:-1]))
                cur_line = [w]
        if cur_line:
            lines.append(" ".join(cur_line))

        y = card_top + 92
        for line in lines[:8]:
            draw.text((x0 + 16, y), line, fill="#E2E8F0", font=font_card_b)
            y += 22

        # Card Key Insight
        if "insight" in b:
            draw.rectangle([x0 + 12, card_top + card_h - 60, x1 - 12, card_top + card_h - 12], fill="#0F172A", outline="#475569", width=1)
            draw.text((x0 + 18, card_top + card_h - 52), "Tavsiya / Qoida:", fill="#94A3B8", font=font_tag)
            draw.text((x0 + 18, card_top + card_h - 36), b["insight"][:32], fill="#F8FAFC", font=font_card_b)

    im.save(filename, quality=95)

print("Infographic helper ready")

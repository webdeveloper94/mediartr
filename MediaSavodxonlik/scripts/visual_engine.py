# -*- coding: utf-8 -*-
"""
Generate diverse diagram types for each topic:
1. Horizontal Process Pipeline / Flow (e.g. 4 steps connected by arrows)
2. Comparison Table / Matrix (2-4 contrasting columns with clear vs elements)
3. Grid Matrix (2x2 Quadrant layout)
4. Checklist / Evaluation Badge Board (vertical scored cards)
5. Hierarchy / Pyramid Layers
6. Core Circle with radiating nodes / Mindmap layout
7. Timeline / Milestone layout
"""

import os
from PIL import Image, ImageDraw, ImageFont

def get_fonts():
    try:
        font_title = ImageFont.truetype("arialbd.ttf", 23)
        font_sub = ImageFont.truetype("arial.ttf", 14)
        font_head = ImageFont.truetype("arialbd.ttf", 16)
        font_body = ImageFont.truetype("arial.ttf", 13)
        font_bold = ImageFont.truetype("arialbd.ttf", 13)
        font_tag = ImageFont.truetype("arialbd.ttf", 11)
        font_big = ImageFont.truetype("arialbd.ttf", 28)
    except:
        font_title = font_sub = font_head = font_body = font_bold = font_tag = font_big = ImageFont.load_default()
    return font_title, font_sub, font_head, font_body, font_bold, font_tag, font_big

def wrap_text(text, max_chars):
    words = text.split()
    lines = []
    cur = []
    for w in words:
        cur.append(w)
        if len(" ".join(cur)) > max_chars:
            lines.append(" ".join(cur[:-1]))
            cur = [w]
    if cur:
        lines.append(" ".join(cur))
    return lines

def draw_header(draw, width, title, subtitle):
    f_title, f_sub, _, _, _, _, _ = get_fonts()
    draw.rectangle([12, 12, width - 12, 470], outline="#334155", width=2)
    draw.rectangle([16, 16, width - 16, 85], fill="#1E293B")
    draw.rectangle([16, 83, width - 16, 86], fill="#2DD4BF")
    draw.text((36, 25), title, fill="#F8FAFC", font=f_title)
    draw.text((36, 56), subtitle, fill="#94A3B8", font=f_sub)

# Layout 1: Horizontal Stepped Flow (4 steps with arrows)
def draw_process_flow(title, subtitle, steps, filename):
    width, height = 980, 480
    im = Image.new("RGB", (width, height), color="#0F172A")
    draw = ImageDraw.Draw(im)
    draw_header(draw, width, title, subtitle)
    f_title, f_sub, f_head, f_body, f_bold, f_tag, f_big = get_fonts()

    n = len(steps)
    margin = 15
    card_w = (width - 40 - (n - 1) * 32) // n
    top = 115
    card_h = 325

    colors = [
        {"border": "#2DD4BF", "badge": "#0F766E", "accent": "#2DD4BF"},
        {"border": "#38BDF8", "badge": "#0369A1", "accent": "#38BDF8"},
        {"border": "#818CF8", "badge": "#4338CA", "accent": "#818CF8"},
        {"border": "#F472B6", "badge": "#BE185D", "accent": "#F472B6"},
    ]

    for i, st in enumerate(steps):
        x0 = 20 + i * (card_w + 32)
        x1 = x0 + card_w
        c = colors[i % len(colors)]

        draw.rectangle([x0, top, x1, top + card_h], fill="#1E293B", outline=c["border"], width=2)
        
        # Step circle number
        draw.ellipse([x0 + 16, top + 16, x0 + 52, top + 52], fill=c["badge"], outline=c["border"], width=1)
        draw.text((x0 + 26, top + 22), f"{i+1}", fill="#FFFFFF", font=f_head)

        draw.text((x0 + 60, top + 24), st.get("tag", f"QADAM 0{i+1}"), fill=c["accent"], font=f_tag)

        # Title
        draw.text((x0 + 16, top + 64), st["title"], fill="#F8FAFC", font=f_head)
        draw.line([x0 + 16, top + 92, x1 - 16, top + 92], fill="#334155", width=1)

        # Body
        lines = wrap_text(st["text"], card_w // 8.5)
        y = top + 104
        for l in lines[:7]:
            draw.text((x0 + 16, y), l, fill="#CBD5E1", font=f_body)
            y += 22

        # Bottom tip
        if "tip" in st:
            draw.rectangle([x0 + 10, top + card_h - 60, x1 - 10, top + card_h - 12], fill="#0F172A", outline="#334155")
            draw.text((x0 + 16, top + card_h - 52), "Tavsiya:", fill="#94A3B8", font=f_tag)
            draw.text((x0 + 16, top + card_h - 36), st["tip"][:28], fill=c["accent"], font=f_bold)

        # Connecting arrow to next card
        if i < n - 1:
            ax = x1 + 6
            ay = top + card_h // 2
            draw.polygon([(ax, ay - 8), (ax + 16, ay), (ax, ay + 8)], fill="#2DD4BF")

    im.save(filename, quality=95)

# Layout 2: 2x2 Grid Quadrant Matrix
def draw_grid_matrix(title, subtitle, quadrants, filename):
    width, height = 980, 480
    im = Image.new("RGB", (width, height), color="#0F172A")
    draw = ImageDraw.Draw(im)
    draw_header(draw, width, title, subtitle)
    f_title, f_sub, f_head, f_body, f_bold, f_tag, f_big = get_fonts()

    qw = (width - 60) // 2
    qh = 155
    coords = [
        (20, 110),
        (20 + qw + 20, 110),
        (20, 110 + qh + 15),
        (20 + qw + 20, 110 + qh + 15),
    ]

    colors = [
        {"border": "#2DD4BF", "badge": "#0F766E", "accent": "#2DD4BF"},
        {"border": "#38BDF8", "badge": "#0369A1", "accent": "#38BDF8"},
        {"border": "#FBBF24", "badge": "#B45309", "accent": "#FBBF24"},
        {"border": "#F43F5E", "badge": "#BE123C", "accent": "#F43F5E"},
    ]

    for i, q in enumerate(quadrants[:4]):
        x0, y0 = coords[i]
        x1, y1 = x0 + qw, y0 + qh
        c = colors[i]

        draw.rectangle([x0, y0, x1, y1], fill="#1E293B", outline=c["border"], width=2)
        
        # Header strip
        draw.rectangle([x0 + 12, y0 + 12, x0 + 110, y0 + 34], fill=c["badge"])
        draw.text((x0 + 18, y0 + 16), q.get("badge", f"MATRITSA 0{i+1}"), fill="#FFFFFF", font=f_tag)

        draw.text((x0 + 120, y0 + 14), q["title"], fill=c["accent"], font=f_head)
        draw.line([x0 + 12, y0 + 42, x1 - 12, y0 + 42], fill="#334155", width=1)

        # Body
        lines = wrap_text(q["text"], qw // 9)
        y = y0 + 50
        for l in lines[:3]:
            draw.text((x0 + 14, y), l, fill="#E2E8F0", font=f_body)
            y += 21

        # Key takeaway footer
        draw.rectangle([x0 + 12, y1 - 34, x1 - 12, y1 - 10], fill="#0F172A")
        draw.text((x0 + 18, y1 - 28), q.get("insight", "Asosiy mezon")[:48], fill=c["accent"], font=f_bold)

    im.save(filename, quality=95)

# Layout 3: Comparative 2-Columns (e.g. TO'G'RI vs NOTO'G'RI or AN'ANAVIY vs RAQAMLI)
def draw_versus_columns(title, subtitle, left_col, right_col, filename):
    width, height = 980, 480
    im = Image.new("RGB", (width, height), color="#0F172A")
    draw = ImageDraw.Draw(im)
    draw_header(draw, width, title, subtitle)
    f_title, f_sub, f_head, f_body, f_bold, f_tag, f_big = get_fonts()

    col_w = (width - 80) // 2
    top = 110
    card_h = 330

    configs = [
        {"x0": 25, "data": left_col, "bg": "#1E293B", "border": "#10B981", "badge": "#065F46", "accent": "#34D399"},
        {"x0": 25 + col_w + 30, "data": right_col, "bg": "#1E293B", "border": "#F43F5E", "badge": "#9F1239", "accent": "#FB7185"}
    ]

    for cfg in configs:
        x0 = cfg["x0"]
        x1 = x0 + col_w
        data = cfg["data"]
        draw.rectangle([x0, top, x1, top + card_h], fill=cfg["bg"], outline=cfg["border"], width=2)

        # Header Badge
        draw.rectangle([x0 + 16, top + 16, x1 - 16, top + 52], fill=cfg["badge"])
        draw.text((x0 + 26, top + 24), data["header"], fill="#FFFFFF", font=f_head)

        # Points
        y = top + 68
        for pt in data["points"][:5]:
            draw.rectangle([x0 + 16, y + 4, x0 + 26, y + 14], fill=cfg["accent"])
            lines = wrap_text(pt, col_w // 9.5)
            for l in lines:
                draw.text((x0 + 34, y), l, fill="#F1F5F9", font=f_body)
                y += 22
            y += 6

        # Bottom result box
        draw.rectangle([x0 + 16, top + card_h - 52, x1 - 16, top + card_h - 14], fill="#0F172A", outline=cfg["border"])
        draw.text((x0 + 24, top + card_h - 40), data["verdict"][:45], fill=cfg["accent"], font=f_bold)

    # VS circle in center
    cx = width // 2
    cy = top + card_h // 2
    draw.ellipse([cx - 24, cy - 24, cx + 24, cy + 24], fill="#0F172A", outline="#64748B", width=2)
    draw.text((cx - 15, cy - 10), "VS", fill="#F8FAFC", font=f_bold)

    im.save(filename, quality=95)

# Layout 4: Layered Pyramid / Hierarchy
def draw_layered_stack(title, subtitle, layers, filename):
    width, height = 980, 480
    im = Image.new("RGB", (width, height), color="#0F172A")
    draw = ImageDraw.Draw(im)
    draw_header(draw, width, title, subtitle)
    f_title, f_sub, f_head, f_body, f_bold, f_tag, f_big = get_fonts()

    n = len(layers)
    top = 110
    total_h = 330
    layer_h = total_h // n
    colors = ["#2DD4BF", "#38BDF8", "#818CF8", "#F472B6", "#FBBF24"]

    for i, lay in enumerate(layers):
        y0 = top + i * layer_h
        y1 = y0 + layer_h - 10
        c = colors[i % len(colors)]

        # Indented stair/layer effect
        indent = (n - 1 - i) * 35 if lay.get("pyramid", False) else 20
        x0 = 20 + indent
        x1 = width - 20 - indent

        draw.rectangle([x0, y0, x1, y1], fill="#1E293B", outline=c, width=2)
        
        # Left tag
        draw.rectangle([x0 + 12, y0 + 10, x0 + 130, y1 - 10], fill="#0F172A", outline=c)
        draw.text((x0 + 20, y0 + 16), lay.get("level", f"DARAJA 0{i+1}"), fill=c, font=f_tag)
        draw.text((x0 + 20, y0 + 34), lay["title"][:14], fill="#F8FAFC", font=f_head)

        # Content text
        lines = wrap_text(lay["text"], (x1 - x0 - 160) // 9.5)
        y = y0 + 14
        for l in lines[:2]:
            draw.text((x0 + 145, y), l, fill="#E2E8F0", font=f_body)
            y += 22

        if "meta" in lay:
            draw.text((x1 - 180, y0 + 24), lay["meta"][:22], fill=c, font=f_bold)

    im.save(filename, quality=95)

# Layout 5: Checklist / Diagnostics Red Flags Inspector
def draw_checklist_inspector(title, subtitle, items, filename):
    width, height = 980, 480
    im = Image.new("RGB", (width, height), color="#0F172A")
    draw = ImageDraw.Draw(im)
    draw_header(draw, width, title, subtitle)
    f_title, f_sub, f_head, f_body, f_bold, f_tag, f_big = get_fonts()

    n = len(items)
    card_w = (width - 40 - (n - 1) * 16) // n
    top = 110
    card_h = 330

    colors = [
        {"status_bg": "#BE123C", "border": "#F43F5E", "title": "#FDA4AF"},
        {"status_bg": "#B45309", "border": "#FBBF24", "title": "#FDE68A"},
        {"status_bg": "#0369A1", "border": "#38BDF8", "title": "#BAE6FD"},
        {"status_bg": "#065F46", "border": "#10B981", "title": "#A7F3D0"}
    ]

    for i, it in enumerate(items):
        x0 = 20 + i * (card_w + 16)
        x1 = x0 + card_w
        c = colors[i % len(colors)]

        draw.rectangle([x0, top, x1, top + card_h], fill="#1E293B", outline=c["border"], width=2)

        # Flag / Status top badge
        draw.rectangle([x0 + 12, top + 14, x1 - 12, top + 46], fill=c["status_bg"])
        draw.text((x0 + 20, top + 22), it.get("badge", f"MEZON 0{i+1}"), fill="#FFFFFF", font=f_bold)

        # Item title
        draw.text((x0 + 14, top + 58), it["title"], fill=c["title"], font=f_head)
        draw.line([x0 + 14, top + 86, x1 - 14, top + 86], fill="#334155", width=1)

        # Details
        lines = wrap_text(it["desc"], card_w // 8.5)
        y = top + 98
        for l in lines[:6]:
            draw.text((x0 + 14, y), l, fill="#E2E8F0", font=f_body)
            y += 22

        # Check action box
        draw.rectangle([x0 + 10, top + card_h - 70, x1 - 10, top + card_h - 12], fill="#0F172A", outline="#475569")
        draw.text((x0 + 16, top + card_h - 62), "Tekshiruv amali:", fill="#94A3B8", font=f_tag)
        c_lines = wrap_text(it["action"], (card_w - 30) // 8.5)
        cy = top + card_h - 44
        for cl in c_lines[:2]:
            draw.text((x0 + 16, cy), cl, fill="#38BDF8", font=f_body)
            cy += 18

    im.save(filename, quality=95)

print("Visual Layout Engine ready!")

from PIL import Image, ImageDraw, ImageFont
import os

# Create a high-res image (256x256)
size = (256, 256)
img = Image.new('RGBA', size, (0, 0, 0, 0))
draw = ImageDraw.Draw(img)

# Background: Rounded rectangle with gradient / solid vibrant color
# Dark rounded background with neon teal border
r = 56
# Outer glow / background
draw.rounded_rectangle([4, 4, 252, 252], radius=r, fill=(11, 24, 40, 255), outline=(45, 212, 191, 255), width=8)

# Inner soft gradient card
draw.rounded_rectangle([20, 20, 236, 236], radius=44, fill=(20, 48, 64, 255))

# Draw 'm.' logo badge in center
# Accent rounded square
draw.rounded_rectangle([52, 52, 204, 204], radius=38, fill=(45, 212, 191, 255))

# Draw 'm.' text
try:
    font = ImageFont.truetype('arialbd.ttf', 115)
except:
    font = ImageFont.load_default()

# Center text 'm.'
text = 'm.'
bbox = draw.textbbox((0, 0), text, font=font)
tw = bbox[2] - bbox[0]
th = bbox[3] - bbox[1]
tx = (256 - tw) // 2 - 4
ty = (256 - th) // 2 - 20

draw.text((tx, ty), text, font=font, fill=(9, 43, 48, 255))

# Small accent dot on top right of the badge
draw.ellipse([184, 44, 206, 66], fill=(167, 139, 250, 255), outline=(255, 255, 255, 255), width=2)

os.makedirs('Assets', exist_ok=True)
ico_path = 'Assets/app.ico'

# Save multi-resolution ICO (256, 128, 64, 48, 32, 16)
sizes = [(256, 256), (128, 128), (64, 64), (48, 48), (32, 32), (16, 16)]
img.save(ico_path, format='ICO', sizes=sizes)
print('Assets/app.ico created successfully!')

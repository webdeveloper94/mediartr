import zipfile
import glob
import re
import os
import shutil

TRANSPARENT_PNG = (
    b'\x89PNG\r\n\x1a\n\x00\x00\x00\rIHDR\x00\x00\x00\x01\x00\x00\x00\x01'
    b'\x08\x06\x00\x00\x00\x1f\x15c4\x00\x00\x00\rIDATx\x9cc`\x00\x00\x00'
    b'\x02\x00\x01H\xaf\xa4q\x00\x00\x00\x00IEND\xaeB`\x82'
)

files = glob.glob('Content/topic-*/slides/*.pptx')
print(f"Fixing {len(files)} pptx files...")

for pptx_path in files:
    tmp_path = pptx_path + '.tmp.pptx'
    with zipfile.ZipFile(pptx_path, 'r') as zin, zipfile.ZipFile(tmp_path, 'w', compression=zipfile.ZIP_DEFLATED) as zout:
        for item in zin.infolist():
            data = zin.read(item.filename)
            
            # Ensure image-1002-2.png is 1x1 transparent PNG
            if 'image-1002-2.png' in item.filename:
                data = TRANSPARENT_PNG
                
            # In slide layouts, remove ANY <p:pic> element whose position is in the bottom-right watermark corner
            # or which refers to image-1002-2.png
            elif item.filename.startswith('ppt/slideLayouts/slideLayout') and item.filename.endswith('.xml'):
                text = data.decode('utf-8', errors='ignore')
                
                # Check each <p:pic> ... </p:pic> block
                def filter_pic(match):
                    pic_content = match.group(0)
                    # If this pic has coords around bottom-right (x="11076875" or similar watermark coords)
                    # or contains rId with preencoded / image-1002-2
                    if '11076875' in pic_content or '1071843' in pic_content or 'rId2' in pic_content or 'rId1' in pic_content:
                        # In slideLayout2, rId1 is background image, rId2 is watermark
                        # In slideLayout3, rId1 is watermark
                        if '11076875' in pic_content or '1071843' in pic_content or 'rId2' in pic_content and '12191695' not in pic_content:
                            return ''
                    return pic_content
                
                new_text = re.sub(r'<p:pic>.*?</p:pic>', filter_pic, text, flags=re.DOTALL)
                if new_text != text:
                    data = new_text.encode('utf-8')
                    
            zout.writestr(item, data)
            
    shutil.move(tmp_path, pptx_path)

print("Slide layouts cleaned!")

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
print(f"Found {len(files)} pptx files.")

for pptx_path in files:
    tmp_path = pptx_path + '.tmp.pptx'
    with zipfile.ZipFile(pptx_path, 'r') as zin, zipfile.ZipFile(tmp_path, 'w', compression=zipfile.ZIP_DEFLATED) as zout:
        for item in zin.infolist():
            data = zin.read(item.filename)
            
            # 1. Replace image-1002-2.png with transparent png
            if 'image-1002-2.png' in item.filename:
                data = TRANSPARENT_PNG
                
            # 2. In slide layouts, remove the shape pointing to rId2 or gamma
            elif item.filename.startswith('ppt/slideLayouts/slideLayout') and item.filename.endswith('.xml'):
                text = data.decode('utf-8', errors='ignore')
                pattern = r'<p:pic>.*?r:embed=[\"\']rId2[\"\'].*?</p:pic>'
                new_text = re.sub(pattern, '', text, flags=re.DOTALL)
                if new_text != text:
                    data = new_text.encode('utf-8')
            
            # 3. In rels, remove gamma hyperlinks
            elif item.filename.startswith('ppt/slideLayouts/_rels/slideLayout') and item.filename.endswith('.xml.rels'):
                text = data.decode('utf-8', errors='ignore')
                pattern = r'<Relationship[^>]*Target=[\"\'][^\"\']*gamma\.app[^\"\']*[\"\'][^>]*/>'
                new_text = re.sub(pattern, '', text, flags=re.IGNORECASE)
                if new_text != text:
                    data = new_text.encode('utf-8')
                    
            zout.writestr(item, data)
            
    shutil.move(tmp_path, pptx_path)

print("All PPTX files successfully updated!")

import zipfile
import re
import os

with zipfile.ZipFile('Content/topic-01/slides/1-mavzu.pptx', 'r') as z:
    for name in z.namelist():
        if name.endswith('.xml'):
            text = z.read(name).decode('utf-8')
            rels_name = os.path.dirname(name) + '/_rels/' + os.path.basename(name) + '.rels'
            if rels_name in z.namelist():
                rels_text = z.read(rels_name).decode('utf-8')
                rels_ids = set(re.findall(r'Id=[\"\'](.*?)[\"\']', rels_text))
            else:
                rels_ids = set()
            
            # Find all references in text
            used_ids = set(re.findall(r'r:(?:id|embed)=[\"\'](.*?)[\"\']', text))
            missing = used_ids - rels_ids
            if missing:
                print(f"MISSING in {rels_name} for {name}: {missing}")

print("Validation completed.")

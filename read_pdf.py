import subprocess
import sys

result = subprocess.run(['python', '-c', '''
import fitz
doc = fitz.open("CLDV6211POE.pdf")
for page in doc:
    print(page.get_text())
doc.close()
'''], capture_output=True, text=True)

print("STDOUT:", result.stdout)
print("STDERR:", result.stderr)
print("Return code:", result.returncode)

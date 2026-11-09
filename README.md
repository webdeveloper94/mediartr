# MediaLab — Media va axborot savodxonligi

WPF / .NET 10, Windows 10 (2004+) yoki Windows 11.
Yo‘nalish: 30610403 — Kompyuter tarmoqlari va IT-servis. Jami 40 soat: 24 nazariy, 16 amaliy.

## Ishga tushirish

```powershell
dotnet run --project MediaSavodxonlik/MediaSavodxonlik.csproj
```

## Admin panel

Dastur oynasida **Ctrl+Shift+A** bosing. Birinchi kirishda kamida 8 belgili parol o‘rnating va takrorlang. Keyingi kirishlarda shu parol so‘raladi; standart parol mavjud emas. Parol tuz va PBKDF2-SHA256 xeshi sifatida saqlanadi. Bu mahalliy interfeys himoyasi; loyiha fayllariga bevosita kirishni operatsion tizim huquqlari boshqaradi.

Admin panelda 20 ta mavzudan birini tanlab nomini saqlang. Ma’ruza, video/media yoki taqdimot turini tanlab fayl yuklang.

## Materiallar

Loyiha ichida ishlaganda fayllar **MediaSavodxonlik/Content/** ichiga nusxalanadi:

```text
Content/
  topics.json
  .admin/credential.json
  topic-01/
    lecture/
    media/
    slides/
```

Asl fayl joyida qoladi; dastur loyiha nusxasidan foydalanadi. Bir xil nom qayta yuklansa, eski nusxa saqlanib, yangi nom yaratiladi. Nusxalash asinxron bajariladi.

Build/publish vaqtida Content natija papkasiga nusxalanadi. Dastur bilan birga shu papkani ham tarqating. Manba loyiha topilmaydigan kompyuterda exe yonidagi Content ishlatiladi. Avvalgi versiyalarning Data papkalari avtomatik ko‘chirilmaydi yoki o‘chirilmaydi.

## Yetti resurs

O‘quvchi sahifalarida yuklash va tahrirlash maydonlari yo‘q.

| Resurs | Kiritish | Ochilish |
|---|---|---|
| Ma’ruza | Admin: PDF, DOCX, TXT | Ichki hujjat ko‘ruvchi |
| Metodlar | TopicPages.cs | O‘qish sahifasi |
| Media | Admin: MP4, WMV, AVI, MOV, MP3, WAV, GIF | Ichki pleyer/rasm ko‘ruvchi |
| Ko‘rgazmalar | TopicPages.cs | O‘qish sahifasi |
| Taqdimot | Admin: PPTX, PDF, JPG/PNG | Ichki ko‘ruvchi |
| Baholash | TopicPages.cs | Savollar va topshiriqlar |
| Glossariy | TopicPages.cs | Atamalar sahifasi |

PDF ko‘ruvchi uchun **Microsoft Edge WebView2 Runtime** kerak. DOCX/PPTX soddalashtirilgan matn va rasm ko‘rinishida ochiladi: asl murakkab dizayn, jadval, joylashuv va slayd animatsiyalari to‘liq saqlanmaydi. Aniq dizayn uchun PDF yuklang. Eski DOC/PPTni DOCX/PPTX yoki PDFga aylantiring. Video uchun MP4 H.264/AAC tavsiya etiladi; boshqa formatlar Windows kodeklariga bog‘liq. GIFning birinchi kadri ko‘rsatiladi.

## Kodda yoziladigan sahifalar

**MediaSavodxonlik/TopicPages.cs** ichidagi lug‘atga matn yozing:

```csharp
[(1, "methods")] = "1-mavzu uchun metodlar...",
[(1, "visuals")] = "Ko‘rgazmali misollar...",
[(1, "assessment")] = "Savollar va topshiriqlar...",
[(1, "glossary")] = "Atama — izoh...",
```

Raqam: mavzu raqami (1–20). O‘zgarganda dastur qayta yig‘iladi. Avtomatik test ballari hozircha hisoblanmaydi.

## Medi

Tanishtirish internetsiz ishlaydi. Mavzuda robotni bosib suhbatni oching. Model: **gpt-6-luna**, reasoning.effort: **none**, OpenAI Responses API.

API kalitini suhbatdagi yashirin sozlama maydoniga kiriting yoki OPENAI_API_KEY muhit sozlamasidan foydalaning. Kalit faylga yozilmaydi. Yuborish bosilganda savol, joriy mavzuning cheklangan tarixi va resurs matnlari OpenAI’ga uzatiladi (store: false). Mavzu nomi topics.json, to‘rtta sahifa TopicPages.cs faylidan olinadi. DOCX/PPTX/TXT matnlari ham qo‘shiladi. PDF/audio/video mazmuni AI uchun avtomatik o‘qilmaydi.

Suhbatlar joriy dastur xotirasida mavzular bo‘yicha alohida turadi. Mavzu almashganda so‘rov bekor qilinadi. Umumiy tashkilot API kalitini tarqatish uchun server vositachisi kerak.

## Tekshiruv va tarqatish

```powershell
dotnet run --project MediaSavodxonlik.SmokeTests -c Release
dotnet publish MediaSavodxonlik/MediaSavodxonlik.csproj -c Release -o artifacts/app
```

Testlar alohida sun’iy kontentdan foydalanadi. API testlari haqiqiy so‘rov yubormaydi. Ichki ko‘ruvchi sinovi qisqa alohida oynada sun’iy PDF/DOCX ochadi.

Mustaqil Windows x64 nashri:

```powershell
dotnet publish MediaSavodxonlik/MediaSavodxonlik.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o artifacts/standalone
```

Mustaqil .NET nashrida ham WebView2 Runtime talab qilinadi.

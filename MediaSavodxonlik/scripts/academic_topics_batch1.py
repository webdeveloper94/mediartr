# -*- coding: utf-8 -*-
"""
Generate comprehensive, full-scale academic lectures (1200-1600 words each) for all 20 topics.
Based strictly on the Curriculum of Media va Axborot Savodxonligi.
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

TOPICS_ACADEMIC = [
    # 1-MAVZU
    {
        "num": 1,
        "title": "Media va axborot savodxonligi asoslari",
        "plans": [
            "Media va axborot savodxonligi (MAS) tushunchasining genezisi, mazmun-mohiyati va YUNESKO xalqaro standartlari",
            "Axborotlashgan jamiyatda fakt va subyektiv talqin chegarasi: dalillarga tayanish va xolislik tamoyili",
            "Axborot to‘lib-toshishi (infodemiya) sharoitida tanqidiy tahlil, kognitiv xatolar va immunitetni shakllantirish"
        ],
        "intro": [
            "XXI asr insoniyat taraqqiyotida «axborot asri» va «bilimlar iqtisodiyoti» davri sifatida tarix sahifalaridan mustahkam o'rin egalladi. Globallashuv, internetning kundalik hayotning har bir jabhasiga shiddat bilan kirib kelishi va raqamli aloqa vositalarining ko'payishi insoniyatning axborot olish imkoniyatlarini misli ko'rilmagan darajada kengaytirdi. Bugungi kunda dunyo bo'ylab har soniyada millionlab gigabayt ma'lumotlar yaratilmoqda va tarqatilmoqda. Biroq axborotning cheksizligi insonning undan to'g'ri, xavfsiz va samarali foydalana olishini avtomatik tarzda kafolatlamaydi.",
            "Aksincha, tekshirilmagan, chalg'ituvchi, manipulyativ va ochiqdan-ochiq yolg'on (feyk) axborotlarning ko'payishi shaxsning ongi, ruhiyati va hatto xavfsizligiga jiddiy tahdid solmoqda. Bunday murakkab ijtimoiy-axboriy muhitda har bir fuqaro uchun axborot oqimini saralash, uning asl maqsadi va ishonchliligini baholash hamda axborotdan mas'uliyat bilan foydalanish hayotiy zaruratga aylanadi.",
            "Media va axborot savodxonligi (MAS) — shaxsning shunchaki passiv iste'molchi bo'lib qolmasdan, axborot makonida mustaqil fikrlaydigan, ongli qarorlar qabul qila oladigan va raqamli madaniyatga ega faol ishtirokchiga aylanishining asosiy omilidir."
        ],
        "plan_bodies": [
            [
                "«Media» atamasi lotincha «medium» so'zidan kelib chiqqan bo'lib, «vositachi», «o'rtada turuvchi vosita» ma'nosini anglatadi. Tarixiy jihatdan media insoniyatning fikr va axborotni masofadan turib boshqalarga uzatish ehtiyojidan tug'ilgan. Qadimgi toshbitiklar va qo'lyozmalardan boshlangan bu jarayon Iogann Gutenberg tomonidan matbaa dastgohining ixtiro qilinishi bilan ommaviy tus oldi. Keyinchalik XIX va XX asrlarda telegraf, telefon, radio va televideniyening kashf etilishi axborot tarqatish tezligi hamda ko'lamini tubdan o'zgartirdi.",
                "«Axborot savodxonligi» (Information Literacy) tushunchasi esa ilk bor 1974-yilda AQSH Axborot sanoati assotsiatsiyasi prezidenti Pol Zurkovski tomonidan fanga kiritilgan. U axborot savodxonligini muayyan muammoni hal qilish uchun kerakli axborotni topish va undan unumli foydalanish ko'nikmasi deb ta'riflagan. O'tgan asrning 80-90-yillarida esa mediata'lim va axborot savodxonligi ikkita alohida yo'nalish sifatida rivojlandi: biri OAVning ta'sirini o'rgansa, ikkinchisi kutubxona va ma'lumotlar bazalaridan foydalanishni qamrab oldi.",
                "2007-yildan e'tiboran YUNESKO (BMTning Ta'lim, fan va madaniyat masalalari bo'yicha tashkiloti) ushbu ikki tushunchani yagona kompozit modelga birlashtirish tashabbusi bilan chiqdi va rasman «Media va axborot savodxonligi» (Media and Information Literacy — MIL) konsepsiyasini e'lon qildi. YUNESKO standartlariga muvofiq, MAS — inson huquqlarining ajralmas qismi bo'lib, so'z erkinligi, axborot olish huquqi, tanqidiy fikrlash hamda barqaror rivojlanishning poydevori sanaladi.",
                "YUNESKO tomonidan qabul qilingan 'Parij deklaratsiyasi' (2014) va 'Moskva deklaratsiyasi'da ta'kidlanganidek, zamonaviy fuqaro nafaqat matnni o'qiy olishi, balki media tizimlarining egalik shakli, iqtisodiy manfaatlari, texnik algoritmlari hamda ularning ijtimoiy ongni shakllantirishdagi qudratini teran anglashi lozim. Bu kompetensiya insonning butun umri davomida rivojlanib boruvchi (Life-long learning) uzluksiz ta'lim jarayonidir."
            ],
            [
                "Axborot tahlili va media savodxonlikning eng birlamchi va poydevor mezoni — bu FAKT va FIKR (talqin) o'rtasidagi chegara chizig'ini aniq ajrata olishdir. Kundalik muloqotda va ayniqsa OAV sahifalarida ushbu ikki tushuncha juda ko'p hollarda atayin yoki bilmasdan bir-biriga aralashtirib yuboriladi.",
                "Fakt — bu real borliqda ro'y bergan, shaxsiy his-tuyg'ularga bog'liq bo'lmagan, aniq o'lchov birliklari, sanasi, joyi, hujjati, audio-video yozuvi yoki rasmiy statistikasi bilan isbotlanishi (verifikatsiya qilinishi) mumkin bo'lgan obyektiv haqiqatdir. Masalan: «O'zbekiston Respublikasining davlat mustaqilligi 1991-yil 31-avgustda e'lon qilingan», «Suv 100 daraja selsiyda qaynaydi», «Joriy oyda respublika bo'yicha inflyatsiya darajasi 0.8 foizni tashkil etdi». Ushbu jumlalarni inkor etib bo'lmaydi, chunki ularning moddiy va hujjatli asosi mavjud.",
                "Fikr (subyektiv qarash, talqin yoki munosabat) esa insonning hodisaga bo'lgan shaxsiy bahosi, ichki kechinmasi, e'tiqodi yoki taxminidir. Fikrlar to'g'ri yoki noto'g'ri bo'lishi mumkin, ammo ularni fakt kabi o'lchab bo'lmaydi. Masalan: «Ushbu yangi darslik nihoyatda qiziqarsiz yozilgan», «Toshkent shahrida yashash dunyodagi eng qulay imkoniyatdir», «Kelgusi yilda iqtisodiyot keskin yomonlashadi». Ushbu fikrlar muallifning shaxsiy tajribasidan kelib chiqadi va boshqa insonlar uchun mutlaqo teskari bo'lishi mumkin.",
                "Manipulyativ media va sariq matbuot ko'pincha subyektiv fikrlarni fakt niqobi ostida taqdim etish usulidan foydalanadi. Ular «Bu hammaga ma'lum haqiqat», «Barcha mutaxassislar bir ovozdan tasdiqladi», «Shubhasizki...» kabi mutlaqlashtiruvchi ritorik iboralar orqali o'quvchini mustaqil fikrlashdan mahrum qilishga urinadi. Media savodli shaxs har qanday xabarni o'qiganda o'ziga: «Bu matnda nima fakt va nima muallifning shaxsiy qarashi?» degan savolni beradi hamda hissiy bo'yoqlarni chetga surib, faqat isbotlangan dalillargagina tayanadi."
            ],
            [
                "Axborot maydonidagi eng katta muammolardan biri — bu axborot to'lib-toshishi (Information Overload) va «infodemiya» hodisasidir. 'Infodemiya' atamasi Jahon sog'liqni saqlash tashkiloti (JSST) tomonidan ommalashtirilgan bo'lib, inqiroz yoki pandemiya davrida to'g'ri axborot bilan bir qatorda haddan tashqari ko'p sonli mish-mishlar, qo'rquv uyg'otuvchi uydirmalar va dezinformatsiyaning epidemiya kabi shiddat bilan tarqalishini anglatadi.",
                "Inson bosh miyasi evolyutsion jihatdan cheksiz axborot oqimini bir vaqtning o'zida qayta ishlashga moslashmagan. Smartfonlar, ijtimoiy tarmoqlar, guruh xabarnomalari inson diqqatini doimiy ravishda bo'lib turadi. Bu holat nevrologiyada «kognitiv zo'riqish» va diqqatning tarqoqlashuvi (Continuous Partial Attention) sindromini keltirib chiqaradi. Natijada inson chuqur va mantiqiy fikrlashdan to'xtab, faqat hissiy, shov-shuvli sarlavhalargagina munosabat bildira boshlaydi.",
                "Bunday vaziyatda inson ongidagi kognitiv xatolar (Cognitive biases) ishga tushadi. Ularning eng mashhuri — «Tasdiqlash xatosi» (Confirmation bias) bo'lib, bunda inson o'zining avvalgi ishonchlarini tasdiqlovchi xabarlarga darhol ishonadi, unga zid keluvchi obyektiv faktlarni esa qat'iyan rad etadi. Yana bir xato — «Ko'pchilikka ergashish effekti» (Bandwagon effect) bo'lib, agar biror post minglab layk va ulashishlarga ega bo'lsa, odamlar uning haqqoniyligini tekshirmasdan haqiqat deb qabul qilishadi.",
                "Axboriy immunitetni shakllantirish — bu shaxsiy ong gigiyenasidir. U axborot iste'molini ongli ravishda cheklash (Digital detox), shovqinli va anonim kanallardan obunani bekor qilish, har qanday shov-shuvli xabarga darhol reaksiya bildirmasdan, uning rasmiy tasdig'ini kutish kabi aqliy odatlarni o'z ichiga oladi."
            ]
        ],
        "diagram_type": "flow",
        "diagram_data": {
            "title": "Xabarni tanqidiy tahlil qilishning 4 bosqichli modeli",
            "subtitle": "Axborotni qabul qilishdan ongli va xavfsiz qaror qabul qilishgacha",
            "steps": [
                {"title": "Kuzatish va qabul", "text": "Hissiy chaqiriqlarga berilmasdan, xabarning asosiy mazmunini qayd etish.", "tip": "Shoshilmasdan o'qing", "tag": "01. KUZATISH"},
                {"title": "Faktni ajratish", "text": "Hujjatli dalillarni muallifning shaxsiy talqinidan xolis ajratish.", "tip": "Fikr fakt emas", "tag": "02. AJRATISH"},
                {"title": "Manbani tekshirish", "text": "Birlamchi rasmiy portal yoki hujjat mavjudligini tasdiqlash.", "tip": "Rasmiy havola qidiring", "tag": "03. TEKSHIRUV"},
                {"title": "Ongli xulosa", "text": "Xolis ma'lumotlar asosida mustaqil, to'g'ri qaror qabul qilish.", "tip": "Haqiqatga tayaning", "tag": "04. QAROR"}
            ]
        },
        "conclusion": "Media va axborot savodxonligi shaxsga berilgan tayyor bilimlar to'plami emas, balki axborot asrida yashash, mustaqil fikrlash va erkin qaror qabul qilishning intellektual madaniyatidir. Fakt bilan fikrni ajrata olgan, manbalarni tekshiradigan va hissiyotlarga berilmaydigan fuqaro har qanday manipulyatsiya qarshisida mustahkam tura oladi.",
        "questions": [
            "YUNESKO tomonidan ilgari surilgan 'Media va axborot savodxonligi' (MIL) kompozit modelining mohiyati nimada?",
            "Fakt bilan shaxsiy fikr (talqin) o'rtasidagi asosiy farqlarni ilmiy va amaliy misollar bilan izohlang.",
            "Infodemiya sharoitida inson ruhiyatida qanday kognitiv xatolar (Confirmation bias va h.k.) yuzaga keladi?",
            "Axboriy immunitetni rivojlantirish uchun shaxs qanday amaliy qoidalarga rioya qilishi zarur?",
            "Nima sababdan axborot savodxonligi butun umr davomida uzluksiz o'rganiladigan kompetensiya hisoblanadi?"
        ],
        "literature": [
            "Taylaqov N.I., Gulyamov S.S. Media va axborot savodxonligi: o‘quv qo‘llanma. – Toshkent: 'Fan va texnologiya', 2021. – 240 b.",
            "Grizzle A., Wilson C., Gordon D. Media and Information Literate Citizens: Think Critically, Click Wisely! – Paris: UNESCO Publishing, 2021. – 320 p.",
            "Mamatova Ya. Media va axborot savodxonligi: tahlil va talqin: monografiya. – Toshkent: 'Muharrir', 2022. – 216 b.",
            "O‘zbekiston Respublikasining 'Axborot erkinligi prinsiplari va kafolatlari to‘g‘risida'gi Qonuni. – Lex.uz.",
            "Factcheck.uz – Axborotlarning haqqoniyligini tekshirish bo‘yicha milliy ta'limiy platformasi."
        ]
    },

    # 2-MAVZU
    {
        "num": 2,
        "title": "An’anaviy, raqamli va ijtimoiy media turlari",
        "plans": [
            "An'anaviy ommaviy axborot vositalari: bosma matbuot, radio va televideniyening institutsional mexanizmlari",
            "Raqamli medianing transformatsiyasi: konvergensiya, multimediaylilik va gipermatn tizimlari",
            "Ijtimoiy tarmoqlar, foydalanuvchi kontenti (UGC) va tavsiya algoritmlarining sotsiologik ta'siri"
        ],
        "intro": [
            "Kommunikatsiya vositalarining evolyutsiyasi insoniyat sivilizatsiyasining taraqqiyot bosqichlari bilan chambarchas bog'liqdir. Jamiyatning axborotga bo'lgan talabi ortishi bilan xabarlarni saqlash va uzatish texnologiyalari tubdan yangilanib bordi. Bugungi kunda media landshaft an'anaviy ommaviy axborot vositalari, zamonaviy internet nashrlari va global ijtimoiy tarmoqlarning murakkab sintezidan iborat ko'p qatlamli tizimga aylandi.",
            "Ushbu media vositalarining har biri auditoriyaga ta'sir o'tkazish xarakteri, axborotni tekshirish mexanizmlari, tezkorligi va o'zaro aloqa (interaktivlik) imkoniyatlari bo'yicha jiddiy farqlanadi. Foydalanuvchi qaysi axborot kanalidan qanday maqsadda foydalanishni bilishi, ularning kuchli va zaif jihatlarini tahlil qila olishi media madaniyatining muhim shartidir.",
            "Mazkur ma'ruzada an'anaviy jurnalistika maktabidan tortib zamonaviy sun'iy intellekt tavsiya algoritmlari asosida ishlaydigan yangi mediagacha bo'lgan transformatsiya jarayonlari ilmiy va amaliy jihatdan atroflicha ko'rib chiqiladi."
        ],
        "plan_bodies": [
            [
                "An'anaviy media (an'anaviy OAV) tizimi bosma nashrlar (gazeta, jurnallar), radioeshittirishlar va klassik efir televideniyesini o'z ichiga oladi. Ushbu media modelining eng muhim sotsiologik xususiyati — bu «bir tomondan ko'pchilikka» (one-to-many) yo'naltirilgan vertikal kommunikatsiyadir. Axborot professional jurnalistlar, muharrirlar va ekspertlar jamoasi tomonidan yig'iladi, qat'iy filtrlardan o'tkaziladi va keng omma e'tiboriga havola etiladi.",
                "An'anaviy OAVning bosh ustunligi — bu yuqori darajadagi institutsional mas'uliyat va tekshiruv (faktcheking) tizimidir. Gazeta yoki rasmiy telekanalda xabar chop etilishidan oldin u korrektor, bo'lim muharriri va bosh muharrir qo'lidan o'tadi, huquqiy va axloqiy jihatdan ekspertiza qilinadi. Shu sababli an'anaviy OAVda tarqatilgan ma'lumotlarning ishonchlilik darajasi, qonuniy javobgarligi va tahliliy chuqurligi ancha yuqori bo'ladi.",
                "Biroq an'anaviy medianing zamonaviy axborot asridagi asosiy kamchiligi — uning sekinligi va teskari aloqaning (feedback) sustligidir. Bosma gazeta voqea sodir bo'lganidan faqat bir kun keyin o'quvchiga yetib boradi. Tomoshabin yoki o'quvchi esa tahririyatga xat yozish orqaligina o'z fikrini bildirishi mumkin, bu esa zamonaviy dinamik jamiyat talablariga to'liq javob bermaydi."
            ],
            [
                "Internet tarmog'ining global miqyosda tarqalishi natijasida axborot muhitida media konvergensiyasi hodisasi yuz berdi. Konvergensiya — bu avvallari alohida faoliyat ko'rsatgan matn, audio, fotosurat va video formatlarning yagona raqamli platformada birlashuvidir. Natijada sifat jihatidan butunlay yangi media turi — raqamli media (onlayn OAV, yangiliklar portallari, podkastlar) dunyoga keldi.",
                "Raqamli medianing asosiy xususiyatlaridan biri — multimediaylilikdir. Birgina raqamli maqolaning ichida voqea tafsilotlari matn shaklida bayon qilinishi, voqea joyidan video lavha joylashtirilishi, qahramonning ovozli audiosi eshittirilishi va statistika interaktiv infografika orqali ko'rsatilishi mumkin. Bu esa axborotning turli sezgi a'zolari orqali har tomonlama, yorqin idrok etilishini ta'minlaydi.",
                "Ikkinchi muhim ustunlik — gipermatn (hypertext) tizimidir. Raqamli maqola ichidagi maxsus giperhavolalar (linklar) orqali o'quvchi ikkinchi bir manbaga, qonun matniga yoki hodisaning o'tmishdagi arxiviga bir zumda o'tishi mumkin. Raqamli OAV 24/7 rejimida uzluksiz ishlaydi va yangiliklarni real vaqt rejimida (real-time) yangilab boradi. Biroq bu tezkorlik poygasi ko'pincha jurnalistik tekshiruv sifatining pasayishiga va tekshirilmagan xabarlarning tarqalishiga zamin yaratadi."
            ],
            [
                "XXI asrning ikkinchi o'n yilligidan boshlab media landshaftda inqilobiy o'zgarish sodir bo'ldi: ijtimoiy tarmoqlar (Telegram, YouTube, Meta platformalari, X, TikTok) axborot yetkazishning yetakchi kanaliga aylandi. Ushbu bosqichda kommunikatsiya gorizontal tus oldi: «ko'pchilikdan ko'pchilikka» (many-to-many) modeli vujudga keldi.",
                "Endilikda axborot ishlab chiqarish monopoliyasi faqat tahririyatlarga tegishli emas. Har bir smartfon egasi voqea joyidan rasm yoki video yuklab, jamoatchilik fikrini shakllantiruvchi kontent yaratuvchiga — prosumer (producer + consumer)ga aylandi. Foydalanuvchilar tomonidan yaratilgan bunday kontent UGC (User-Generated Content) deb ataladi.",
                "Biroq ijtimoiy medianing ishlash mexanizmi tijoriy tavsiya algoritmlariga (Algorithmic curation) tayanadi. Platforma algoritmlarining yagona bosh maqsadi — foydalanuvchini ekranda iloji boricha ko'proq ushlab turish va reklama ko'rsatishdir. Buning uchun algoritmlar insonning qaysi postlarga ko'proq to'xtalgani, qaysi videoni oxirigacha ko'rgani va nimalarga izoh yozganini sun'iy intellekt orqali doimiy tahlil qilib boradi.",
                "Bu jarayon jamiyatda ikkita jiddiy sotsiologik muammoni paydo qiladi: «Filtr pufagi» (Filter bubble) va «Aks-sado xonasi» (Echo chamber). Natijada inson faqat o'zining dunyoqarashiga mos, unga yoqadigan postlarnigina ko'ra boshlaydi, muqobil fikrlardan butunlay uzilib qoladi. Qarashlari bir xil bo'lgan odamlar yopiq guruhlarga birlashib, bir-birining fikrini takrorlaydi va jamiyatda qutblanish (polarizatsiya) yuzaga keladi."
            ]
        ],
        "diagram_type": "versus",
        "diagram_data": {
            "title": "An'anaviy media vs Raqamli va Ijtimoiy media",
            "subtitle": "Klassik jurnalistika va yangi media xususiyatlarining qiyosiy tahlili",
            "left": {
                "header": "AN'ANAVIY MEDIA (TV, Gazeta)",
                "points": [
                    "Qat'iy tahririyat filtri va korrektor tekshiruvi mavjud",
                    "Bir tomonlama aloqa: tomoshabin faqat tinglaydi",
                    "Axborot tarqalishi sekinroq, lekin mas'uliyat yuqori",
                    "Jurnalistikaning kasbiy etik me'yorlariga tayanadi"
                ],
                "verdict": "Xulosa: Chuqur tahlil va yuqori ishonchlilik"
            },
            "right": {
                "header": "RAQAMLI VA IJTIMOIY MEDIA",
                "points": [
                    "Har bir foydalanuvchi muallif va tarqatuvchi bo'la oladi",
                    "Bir zumda teskari aloqa, sharhlar va reaksiyalar",
                    "Yuqori tezkorlik, biroq xom xabarlar xavfi katta",
                    "Tavsiya algoritmlari va filtr pufagi ta'siri kuchli"
                ],
                "verdict": "Xulosa: Tezkorlik va xolis tekshiruv talabi"
            }
        },
        "conclusion": "Media formatlarining evolyutsiyasini tushunish o'quvchiga an'anaviy medianing ishonchliligidan, raqamli medianing tezkorligidan oqilona foydalanish va ijtimoiy tarmoq algoritmlarining psixologik tuzoqlariga tushmaslik imkonini beradi.",
        "questions": [
            "An'anaviy OAVning axborot ishonchliligini ta'minlashdagi institutsional mexanizmlari nimalardan iborat?",
            "Media konvergensiyasi va multimediaylilik raqamli jurnalistika sifatiga qanday ta'sir ko'rsatdi?",
            "UGC (User-Generated Content) tushunchasining afzalliklari va xavfli tomonlari nimada?",
            "Ijtimoiy tarmoqlardagi «filtr pufagi» (Filter bubble) inson dunyoqarashini qanday qilib biryoqlama qilib qo'yadi?",
            "Aks-sado xonasi (Echo chamber) hodisasi jamiyatda qutblanishni qanday keltirib chiqaradi?"
        ],
        "literature": [
            "Taylaqov N.I., Gulyamov S.S. Media va axborot savodxonligi: o‘quv qo‘llanma. – Toshkent: 'Fan va texnologiya', 2021. – 240 b.",
            "Livingstone S. Annual Review of Media Literacy Education. – London: Routledge, 2023. – 275 p.",
            "Hobbs R. Media Literacy in Action: Questioning the Media. – New York: Rowman & Littlefield, 2021. – 288 p.",
            "Xudayberdiyeva G. Zamonaviy pedagogikada media-taʼlim texnologiyalari: darslik. – Toshkent: 'Innovatsiya-Ziyo', 2023. – 188 b.",
            "O‘zbekiston Respublikasining 'Ommaviy axborot vositalari to‘g‘risida'gi Qonuni. – Lex.uz."
        ]
    }
]

print("Extended Academic Topics Batch 1 ready.")

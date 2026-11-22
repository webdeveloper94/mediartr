# -*- coding: utf-8 -*-
"""
Full rich topic definitions for Topics 6 to 20.
"""

from rich_topics_data import RICH_TOPICS

MORE_RICH_TOPICS = [
    # 6-MAVZU
    {
        "num": 6,
        "title": "Kalit so‘z va filtrlar yordamida axborot izlash",
        "plans": [
            "Axborot izlashda kalit so‘zlarni to‘g‘ri shakllantirish va semantik bog‘liqliklar",
            "Qidiruv tizimlarining amaliy filtrlari: vaqt, geografik hudud va kontent formati",
            "Multimodal qidiruv: teskari tasvir (Reverse Image Search) va audio orqali qidirish usullari"
        ],
        "intro": "Amaliy laboratoriya mashg'ulotlarida eng ko'p talab etiladigan ko'nikmalardan biri — axborot qidiruvining aniqligi va tezligidir. Oddiy so'rovlar millionlab foydasiz havolalarni qaytarsa, aniq kalit so'zlar va to'g'ri sozlangan filtrlar bir necha soniyada zaruriy natijaga olib chiqadi.",
        "plan_contents": [
            [
                "Kalit so'zlarni shakllantirishda foydalanuvchilar yo'l qo'yadigan eng katta xato — qidiruv satriga uzun savol yoki gap yozishdir. Qidiruv algoritmlari tabiiy til sintaksisidan ko'ra mustaqil semantik birliklar (keywords) bilan ancha samarali ishlaydi.",
                "Mavzuning o'zagini tashkil etuvchi 2-3 ta asosiy terminni ajratib olish va ularning sinonimlarini (shu jumladan ingliz va rus tillaridagi muqobillarini) qo'llash qidiruv doirasini professional darajada kengaytiradi.",
                "Shuningdek, ixtisoslashtirilgan ilmiy bazalarda (Google Scholar, ScienceDirect, ZiyoNET) mavzu bo'yicha qabul qilingan rasmiy ilmiy teglardan foydalanish tavsiya etiladi."
            ],
            [
                "Qidiruv tizimlarining 'Tools' (Asboblar) menyusida vaqt filtrini to'g'ri sozlash muhim amaliy ahamiyatga ega. Masalan, oxirgi 24 soat, oxirgi 1 oy yoki oxirgi 1 yil oralig'ini belgilash orqali eskirgan ma'lumotlar avtomatik elanadi.",
                "Hududiy filtr esa milliy qonunchilik yoki mintaqaviy statistikani o'rganishda xorijiy davlatlar ma'lumotlari aralashib ketishining oldini oladi.",
                "Format filtri orqali foydalanuvchi to'g'ridan-to'g'ri kitoblar, hisobotlar (.pdf), taqdimotlar (.pptx) yoki jadvallar (.xlsx) ko'rinishidagi fayllarni bir zumda yuklab olish imkoniga ega bo'ladi."
            ],
            [
                "Zamonaviy qidiruv faqat matn bilan cheklanmaydi. Multimodal qidiruv texnologiyalari (Google Lens, Yandex Images) tasvirlarni tahlil qilish imkonini beradi.",
                "Teskari tasvir qidiruvi (Reverse image search) orqali rasm internetga ilk bor qachon yuklangani, undagi qahramonning asl shaxsi va tasvir boshqa veb-saytlarda qanday kontekstda ishlatilgani aniqlanadi.",
                "Ushbu usul montaj qilingan, kontekstdan uzilgan yoki eski voqealarga tegishli suratlarni yangi deb ko'rsatishga urinishlarni fosh etishda faktchekerlarning bosh qurolidir."
            ]
        ],
        "diagram_type": "flow",
        "diagram_data": {
            "title": "Kalit so'z va filtrlar bilan ishlash algoritmi",
            "subtitle": "Keng mavzudan aniq va tasdiqlangan hujjatgacha",
            "steps": [
                {"title": "Termin tanlash", "text": "Mavzuning tayanch terminlarini ajratish va sinonimlarini tuzish.", "tip": "Savol bermang, termin yozing", "tag": "01. KALIT SO'Z"},
                {"title": "Filtrlarni yoqish", "text": "Vaqt (oxirgi 1 yil) va format (.pdf, .docx) filtrlarini belgilash.", "tip": "filetype:pdf dan foydalaning", "tag": "02. FILTRLASH"},
                {"title": "Multimodal tekshiruv", "text": "Grafik va tasvirlarni Google Lens orqali teskari qidiruvda tekshirish.", "tip": "Asl sanani aniqlang", "tag": "03. REVERSE"},
                {"title": "Hujjatni yuklash", "text": "Rasmiy domendagi birlamchi PDF hisobotni tanlab olish.", "tip": "Birlamchi manba", "tag": "04. NATIJA"}
            ]
        },
        "conclusion": "To'g'ri kalit so'z va filtrlardan foydalanish qidiruv unumdorligini keskin oshiradi va foydalanuvchini axborot xatoliklaridan asraydi.",
        "questions": [
            "Qidiruvda sinonimlar va xalqaro atamalarning roli qanday?",
            "Teskari tasvir qidiruvi (Reverse Image Search) qanday amaliy vazifalarni hal qiladi?",
            "Vaqt filtri nima sababdan doimo nazorat qilinishi kerak?"
        ]
    },

    # 7-MAVZU
    {
        "num": 7,
        "title": "Bir nechta manbani taqqoslash va vazifaga mosini tanlash",
        "plans": [
            "Axborot tahlilida «Lateral reading» (Yonlama o‘qish) usuli va uning afzalliklari",
            "Ziddiyatli axborotlarni solishtirish: konsensus, mustaqil dalil va rasmiy pozitsiyalar",
            "Olingan vazifaning maqsadi va ilmiy talablariga mos optimal manbani tanlash"
        ],
        "intro": "Bitta manbaga tayanib xulosa chiqarish — intellektual xatoliklarning eng keng tarqalgan sababidir. Har bir axborot kanali hodisaga o'z prizmasi orqali qaraydi. Shu bois professional faktcheking va ilmiy tahlilda ko'p manbali qiyosiy yondashuv shart qilib qo'yilgan.",
        "plan_contents": [
            [
                "Klassik vertikal o'qishda o'quvchi sahifani ochadi va uning dizayniga, matn hajmiga qarab unga ishonish-ishonmaslik haqida xulosa qiladi. Professional tadqiqotchilar esa 'Lateral reading' (Yonlama o'qish) usulini qo'llaydilar.",
                "Yonlama o'qishning mohiyati shundaki, ochilgan saytda uzoq qolib ketmasdan, darhol brauzerda yangi tablar ochiladi va ushbu manba, uning muassisi va moliyalashtiruvchisi haqida mustaqil tashkilotlar hamda boshqa OAVlar nima deb yozganligi o'rganiladi.",
                "Bu usul o'quvchiga chiroyli dizayn ortiga yashiringan firibgar yoki buyurtma saytlarni bir necha daqiqada fosh qilish imkoniyatini beradi."
            ],
            [
                "Turli manbalarda bir xil hodisa bo'yicha turlicha statistik raqamlar yoki qarama-qarshi xabarlar berilgan holatlarda «Triangulyatsiya» (kamida 3 ta mustaqil manba) qoidasi qo'llaniladi.",
                "Birinchi manba — bevosita davlat organining rasmiy bayonoti yoki qonuniy hujjati. Ikkinchi manba — mustaqil xalqaro va mahalliy jurnalistik surishtiruvlar. Uchinchi manba — mazkur sohada uzoq yillik obro'ga ega mustaqil ekspertlar fikri.",
                "Ushbu uch manbaning kesishgan nuqtasi (konsensus) eng haqqoniy haqiqatga yaqin hisoblanadi."
            ],
            [
                "Vazifaga mos manbani tanlash qo'yilgan maqsadga bog'liq. Agar siz talaba sifatida ilmiy kurs ishi yoki dissertatsiya yozayotgan bo'lsangiz, OAV yangiliklari asosiy manba bo'la olmaydi, balki Scopus yoki Web of Science indeksidagi maqolalar tanlanishi kerak.",
                "Agar amaliy kasbiy instruksiya zarur bo'lsa, davlat standartlari (GOST, O'zDSt) va normativ-huquqiy hujjatlar (Lex.uz) tanlanadi.",
                "Shaxsiy qarorlar uchun esa mustaqil foydalanuvchilarning xolis amaliy tajribasi va sinov hisobotlari mos keladi."
            ]
        ],
        "diagram_type": "versus",
        "diagram_data": {
            "title": "Vertikal o'qish vs Lateral (Yonlama) o'qish",
            "subtitle": "Klassik o'quvchi va professional tahlilchi harakatlarining qiyosi",
            "left": {
                "header": "KLASSIK VERTIKAL O'QISH",
                "points": [
                    "Bitta sayt sahifasida qolib ketadi va matnni to'liq o'qiydi",
                    "Saytning chiroyli dizayni va rasmlariga aldanadi",
                    "Muallif va tashkilotning kimligini tashqi manbadan tekshirmaydi",
                    "Yolg'on va manipulyatsiyaga uchrash ehtimoli yuqori"
                ],
                "verdict": "Xavf: Bir tomonlama subyektiv xulosa"
            },
            "right": {
                "header": "LATERAL (YONLAMA) O'QISH",
                "points": [
                    "Darhol yangi tablar ochib, manbaning kimligini qidiradi",
                    "Mustaqil ensiklopediya va ekspertlar sharhini solishtiradi",
                    "Saytdan tashqaridagi faktlarga tayanib baho beradi",
                    "Soxta va buyurtma kontentni bir necha daqiqada fosh etadi"
                ],
                "verdict": "Afzallik: Xolis va tasdiqlangan haqiqat"
            }
        },
        "conclusion": "Bir nechta manbalarni qiyosiy taqqoslash xolis haqiqatni kashf etishning yagona ilmiy metodidir.",
        "questions": [
            "Lateral reading usulining asosiy tamoyili nimadan iborat?",
            "Axborot tahlilida 'Triangulyatsiya' qoidasi qanday qo'llaniladi?",
            "Ilmiy tadqiqot uchun manba tanlashda qaysi mezonlar yetakchi sanaladi?"
        ]
    },

    # 8-MAVZU
    {
        "num": 8,
        "title": "Media xabarining muallifi, maqsadi va auditoriyasi",
        "plans": [
            "Xabar muallifining shaxsi, manfaatlar to‘qnashuvi va moliyalashtirish manbalari",
            "Maqsadli auditoriyani o‘rganish: psixologik segmentatsiya va ta’sir ko‘rsatish vositalari",
            "Media maydonida kun tartibini shakllantirish (Agenda setting) va yashirin maqsadlar"
        ],
        "intro": "Hech bir media matni shunchaki bo'shliqda paydo bo'lmaydi. Har bir kontent ortida uning muallifi, homiysi, aniq yo'naltirilgan auditoriyasi va erishmoqchi bo'lgan maqsadi turadi. Ushbu kommunikativ uchlikni tushunish xabarning tub mohiyatini ochib beradi.",
        "plan_contents": [
            [
                "Muallif kim? Ushbu savol har bir xabarni o'qishda birlamchi bo'lishi kerak. Muallifning mustaqilligi uning moliyaviy va siyosiy erkinligi bilan uzviy bog'liqdir.",
                "Agar ma'lum bir dori vositasi haqidagi 'ilmiy' maqola farmatsevtika kompaniyasi tomonidan moliyalashtirilgan fond tomonidan chop etilgan bo'lsa, bu yerda manfaatlar to'qnashuvi mavjud bo'ladi.",
                "Shu bois nufuzli xalqaro nashrlarda mualliflar o'zlarining moliyalashtirish manbalarini ochiq ko'rsatishlari qonuniy majburiyat hisoblanadi."
            ],
            [
                "Har bir media xabari ma'lum bir auditoriyaga moslab 'o'raladi'. Yoshi, jinsi, kasbi, hududi va dunyoqarashiga ko'ra auditoriya segmentlarga bo'linadi.",
                "Yoshlar uchun vizual dinamika, o'tkir hazil va soddalashtirilgan til qo'llanilsa, tadbirkorlar va mutaxassislar uchun statistika, raqamlar va prognozlar tanlanadi.",
                "Tahlilchi xabarni o'qiyotganda: 'Muallif nima uchun aynan shu ohangni va shu so'zlarni tanladi? U menga qanday tuyg'uni singdirmoqchi?' degan savolni berishi zarur."
            ],
            [
                "Kun tartibini shakllantirish (Agenda setting) nazariyasiga ko'ra, media odamlarga nima deb o'ylashni buyura olmasligi mumkin, ammo nima haqida o'ylash kerakligini belgilab berishda juda katta qudratga ega.",
                "Agar barcha yangiliklar lentalari bir vaqtning o'zida ikkinchi darajali shov-shuvli voqeani tinimsiz ko'rsatsa, jamoatchilik e'tibori jiddiy ijtimoiy-iqtisodiy masalalardan chalg'iydi.",
                "Muallifning yashirin maqsadini anglash — bu ushbu kontent orqali o'quvchida qanday xatti-harakat uyg'otilmoqchi ekanligini (sotib olish, ovoz berish, qo'rqish yoki befarq bo'lish) bilishdir."
            ]
        ],
        "diagram_type": "grid",
        "diagram_data": {
            "title": "Media tahlilining kommunikativ uchligi",
            "subtitle": "Xabarni to'liq tushunish uchun 4 ta tizimli savol",
            "quadrants": [
                {"title": "Kim yaratdi? (Muallif)", "badge": "MUALLIF", "text": "Muallif shaxsi, uning kasbiy tajribasi, qarashlari va moliyaviy homiysi.", "insight": "Manfaatlar to'qnashuvini tekshiring"},
                {"title": "Kim uchun? (Auditoriya)", "badge": "AUDITORIYA", "text": "Xabar kimga mo'ljallangan? Qanday psixologik 'richaglar' qo'llanilgan?", "insight": "Ta'sir nuqtalarini aniqlang"},
                {"title": "Nima maqsadda? (Maqsad)", "badge": "MAQSAD", "text": "Axborot berishmi, ishontirishmi, sotishmi yoki g'azab uyg'otishmi?", "insight": "Asl motivatsiyani ko'ring"},
                {"title": "Nima tushirib qoldirildi?", "badge": "KONTEKST", "text": "Qaysi muhim faktlar va qarama-qarshi tomon fikrlari aytilmadi?", "insight": "Yashirilgan ma'lumotni qidiring"}
            ]
        },
        "conclusion": "Muallif, auditoriya va maqsad uyg'unligini o'rganish orqali manipulyativ ta'sirlardan himoyalanish mumkin.",
        "questions": [
            "Manfaatlar to'qnashuvi (Conflict of interest) nima va u axborot sifatiga qanday ta'sir qiladi?",
            "Kun tartibini shakllantirish (Agenda setting) nazariyasi nimani tushuntiradi?",
            "Xabarda tushirib qoldirilgan faktlarni aniqlash nima uchun muhim?"
        ]
    },

    # 9-MAVZU
    {
        "num": 9,
        "title": "Feyk xabar, dezinformatsiya va manipulyatsiya belgilari",
        "plans": [
            "Axborot buzilishining uch turi: misinformatsiya, dezinformatsiya va malinformatsiya",
            "Feyk (yolg‘on) xabarlarning tashqi va ichki xarakterli belgilari (Red flags)",
            "Sun'iy intellekt, Deepfake texnologiyalari va raqamli manipulyatsiya xatarlari"
        ],
        "intro": "Raqamli texnologiyalar rivoji axborot tarqatishni osonlashtirish bilan birga, keng ko'lamli soxta xabarlar va dezinformatsiya xurujlarining ko'payishiga ham olib keldi. Yolg'on xabarlar insonlar ongini zaharlaydi, vahima uyg'otadi va jamiyat barqarorligiga tahdid soladi.",
        "plan_contents": [
            [
                "Axborot buzilishlarini Yevropa Kengashi tasnifiga ko'ra uchta toifaga ajratish qabul qilingan:",
                "1. Misinformatsiya (Misinformation) — adashib, bilmasdan tarqatilgan noto'g'ri xabar. Muallifda yomon niyat yoki birovga zarar yetkazish maqsadi bo'lmaydi (masalan, eski voqeani adashib yangi deb ulashish).",
                "2. Dezinformatsiya (Disinformation) — atayin, qasddan zarar yetkazish, aldash, siyosiy yoki moddiy manfaat ko'rish maqsadida to'qib chiqarilgan soxta axborot.",
                "3. Malinformatsiya (Malinformation) — aslida haqiqiy bo'lgan shaxsiy yoki nozik ma'lumotlarni noqonuniy ommaga sizdirib, kishiga zarar yetkazish (kiberbulling, shantaj)."
            ],
            [
                "Feyk xabarlarni oddiy ko'z bilan aniqlash mumkin bo'lgan qator xarakterli belgilari (Red flags) mavjud:",
                "Sarlavha qichqiruvchi va hissiyotlarga asoslangan bo'ladi: «Daxshat!», «Hamma bilsin!», «Prezident yangi qaror chiqardi — zudlik bilan barcha kartalar yopiladi!». Matnda harflar katta-katta (CAPS LOCK) yoziladi.",
                "Aniq ism-familiya, rasmiy idora nomi va sana ko'rsatilmaydi. O'rniga mavhum iboralar qo'llanadi: «Britaniyalik olimlar aniqladi», «Manbalarning aytishicha», «Guruhdagilar tezda tarqatinglar».",
                "Matn oxirida doimo shoshiltirish chaqirig'i bo'ladi: «Ushbu xabarni 10 kishiga yuboring, aks holda profilingiz o'chiriladi». Bunday iboralar soxtalikning 100% belgisidir."
            ],
            [
                "Bugungi kunda sun'iy intellekt yordamida yaratilayotgan Deepfake (chuqur soxtalashtirish) texnologiyalari jiddiy xavf tug'dirmoqda. Neyrotarmoqlar orqali taniqli davlat arbobi yoki san'atkorning ovozi va yuzini real vaqt rejimida boshqa odamga nusxalash mumkin.",
                "Deepfake'larni aniqlash uchun yuz qirralariga, ko'z pirpirashiga, lab harakati va ovozning sinxronligiga hamda quloq va soch sohalaridagi piksellanishga e'tibor qaratiladi.",
                "Shuningdek, matn generatsiyasi (ChatGPT va boshqalar) orqali soniyalar ichida yuzlab soxta 'ilmiy' maqolalar yaratilishi mumkin, bu esa akademik savodxonlikni yanada talabchan qiladi."
            ]
        ],
        "diagram_type": "inspector",
        "diagram_data": {
            "title": "Feyk va dezinformatsiya signallari (Red Flags)",
            "subtitle": "Yolg'on xabarni bir qarashda fosh etuvchi 4 ta asosiy belgi",
            "items": [
                {"title": "Hissiy Bosim", "badge": "VAHIMA", "desc": "«Daxshat!», «Tezda tarqating!» kabi vahima va shoshqaloqlik uyg'otish.", "action": "Hissiyotga berilmang"},
                {"title": "Manbaning Yo'qligi", "badge": "ANONIM", "desc": "Rasmiy tashkilot, sana va muallif ko'rsatilmagan mavhum matn.", "action": "Rasmiy saytni oching"},
                {"title": "Mantiqiy Xatolar", "badge": "MONTAJ", "desc": "Grammatik savodsizlik, eski yillardagi fotomontaj qilingan suratlar.", "action": "Rasmni teskari qidiring"},
                {"title": "Shoshilinch Chaqiriq", "badge": "MANIPULYATSIYA", "desc": "«10 kishiga yuboring», «Ertadan pul olinadi» kabi virusli zanjirlar.", "action": "Ulashishni to'xtating"}
            ]
        },
        "conclusion": "Feyk xabarlarni tarqatmaslik — har bir internet foydalanuvchisining ijtimoiy burchi va raqamli madaniyatidir.",
        "questions": [
            "Misinformatsiya bilan dezinformatsiyaning tub farqi nimada?",
            "Feyk xabarlarning 3 ta asosiy alomatini misollar bilan ko'rsating.",
            "Deepfake texnologiyasi qanday ishlaydi va uni qanday aniqlash mumkin?"
        ]
    },

    # 10-MAVZU
    {
        "num": 10,
        "title": "Xabarni asl manba, sana va mustaqil dalillar orqali tekshirish",
        "plans": [
            "Faktcheking amaliyoti: xabar zanjiri bo‘yicha asl manbagacha (Trace back) borish",
            "Sana manipulyatsiyasi va xronologik faktlarni raqamli arxivlar yordamida tekshirish",
            "Mustaqil dalillar bazasi, faktcheking platformalari va EXIF metama'lumotlari tahlili"
        ],
        "intro": "Faktcheking — bu shunchaki quruq shubha emas, balki qat'iy tekshiruv metodologiyasiga ega amaliy kasbiy mahoratdir. Ushbu darsda xabarning haqiqiyligini bosqichma-bosqich tekshirishning professional algoritmlari o'rganiladi.",
        "plan_contents": [
            [
                "Axborot tarmoqda bir manbadan ikkinchisiga o'tganda ma'lum darajada o'zgaradi, qisqaradi va bo'rttiriladi. Bu 'Buzuq telefon' effektidir. Faktchekerning birinchi vazifasi — xabar zanjirini orqaga qarab kuzatish (Trace back) va dastlabki xabar kim tomonidan chiqarilganini topishdir.",
                "Ko'pincha xorijiy OAV maqolasi noto'g'ri yoki kontekstdan uzib tarjima qilinadi. Birlamchi xorijiy nashr topilganda, sarlavha va matnning asl ma'nosi butunlay boshqacha ekanligi ma'lum bo'ladi.",
                "O'zbekiston amaliyotida normativ hujjatlar bo'yicha birlamchi va yagona ishonchli manba Lex.uz milliy qonunchilik bazasi hamda vazirliklarning rasmiy matbuot xizmatlari hisoblanadi."
            ],
            [
                "Feyk xabarlarning 40% dan ortig'i sana manipulyatsiyasiga tayanadi. 5-10 yil oldin boshqa davlatda ro'y bergan avtohalokat, suv toshqini yoki bino qulashi videosi 'Kecha falon viloyatda sodir bo'ldi' degan yozuv bilan qayta tarqatiladi.",
                "Raqamli arxivlar, xususan, Internet Archive (Wayback Machine - archive.org) orqali istalgan veb-saytning o'tmishdagi (1 oy, 1 yil yoki 10 yil oldingi) holatini ko'rish mumkin.",
                "Agar sayt egasi ma'lumotni o'chirib yuborgan yoki tahrirlagan bo'lsa, kesh va arxiv nusxasi faktchekerga haqiqatni isbotlashga yordam beradi."
            ],
            [
                "Fotomateriallarni tekshirishda EXIF (Exchangeable Image File Format) metama'lumotlari tahlil qilinadi. Har bir raqamli fotosurat o'zida suratga olingan sana, vaqt, kamera modeli va GPS koordinatalarini saqlaydi.",
                "FotoForensics kabi ixtisoslashgan vositalar tasvir piksellaridagi tahrirlangan (Error Level Analysis - ELA) joylarni ko'rsatib, fotoshop qilingan elementlarni fosh etadi.",
                "Factcheck.uz kabi xolis platformalar mutaxassislari tomonidan tayyorlangan tekshiruv hisobotlari bilan tanishib borish har bir mutaxassis uchun foydalidir."
            ]
        ],
        "diagram_type": "flow",
        "diagram_data": {
            "title": "Faktcheking tekshiruvining 4 bosqichli zanjiri",
            "subtitle": "Shubhali xabardan daliliy xulosagacha amaliy harakatlar",
            "steps": [
                {"title": "Birlamchi manba", "text": "Xabarni birinchi e'lon qilgan sayt yoki shaxsni aniqlash.", "tip": "Zanjir bo'ylab boring", "tag": "01. MANBA"},
                {"title": "Sana va joylashuv", "text": "Voqea qachon va qayerda ro'y berganini xronologik solishtirish.", "tip": "Eski videolarga aldanmang", "tag": "02. XRONOLOGIYA"},
                {"title": "Tasvir diagnostikasi", "text": "FotoForensics va Google Lens orqali fotomontajni tekshirish.", "tip": "EXIF ma'lumotlarini ko'ring", "tag": "03. DIAGNOSTIKA"},
                {"title": "Ekspert xulosasi", "text": "Rasmiy idoralar va Factcheck.uz portali bilan tasdiqlash.", "tip": "Fakt bilan yakunlang", "tag": "04. TASDIQ"}
            ]
        },
        "conclusion": "Asl manba va sanani tekshirish — har qanday xabarning haqqoniyligini aniqlashning pishiq asosidir.",
        "questions": [
            "Asl manbaga qaytish (Trace back) qanday amalga oshiriladi?",
            "Wayback Machine raqamli arxivi faktchekerga nima uchun kerak?",
            "EXIF metama'lumotlari orqali qanday ma'lumotlarni o'rganish mumkin?"
        ]
    },

    # 11-MAVZU
    {
        "num": 11,
        "title": "Post, reklama yoki videoni tanqidiy tahlil qilish",
        "plans": [
            "Vizual grammatika: rakurs, rang, yoritish va kadr kompozitsiyasining psixologik ta'siri",
            "Matnli ritorika: manipulyativ epitetlar, kontrastlar va mantiqiy xatolar (Fallacies)",
            "Audiovizual kontentni dekonstruksiya qilish: tasvir, tovush va ma'no qatlamlarini ajratish"
        ],
        "intro": "Vizual xabarlar inson ongiga matnga qaraganda bevosita va hissiy ta'sir ko'rsatadi. Kadrning qanday olingani, undagi musiqa va tanlangan so'zlar auditoriya fikrini boshqarish quroliga aylanishi mumkin. Dekonstruksiya — bu vizual sehrni fosh etuvchi tahlil usulidir.",
        "plan_contents": [
            [
                "Vizual grammatika qonunlariga ko'ra, kameraning har bir harakati muayyan ma'noni tashiydi. Masalan, pastdan yuqoriga qaratib olingan kadr (Low angle) qahramonni kuchli, qudratli va xavfli qilib ko'rsatsa, yuqoridan pastga olingan kadr (High angle) uni ojiz, zaif va himoyasiz qilib gavdalantiradi.",
                "Ranglar gammasi ham tomoshabin kayfiyatini yo'naltiradi: sovuq ko'k va kulrang tuslar tushkunlik va xavfni, yorqin iliq sariq va to'q sariq ranglar esa quvonch va xavfsizlikni uyg'otadi.",
                "Reklama va targ'ibot roliklarida ushbu vizual usullardan tomoshabinni ongsiz ravishda xulosa chiqarishga undash uchun ustalik bilan foydalaniladi."
            ],
            [
                "Matnli ritorikada eng ko'p uchraydigan manipulyatsiya usullari — bu mantiqiy xatolar (Logical Fallacies)dir. Eng mashhur misollar:",
                "«Ad Hominem» (Shaxsiyatga o'tish) — dalilni muhokama qilish o'rniga gapiruvchining kiyimi, millati yoki shaxsiy hayotini haqoratlash.",
                "«Soxta dilemma» — vaziyatni faqat ikki qutbga ajratish: «Yo siz bizning fikrimizga qo'shilasiz, yo vatan xoini bo'lasiz». Aslida o'rtada yuzlab muqobil to'g'ri yechimlar mavjud bo'ladi.",
                "«Somon qo'g'irchoq» (Straw Man) — suhbatdoshning fikrini atayin bo'rttirib va buzib ko'rsatib, keyin o'sha soxta fikrni oson yengish."
            ],
            [
                "Audiovizual dekonstruksiya amaliyotida videorolik uchta mustaqil qatlamga ajratib tahlil qilinadi: vizual kadr, so'zlar matni va tovush foni.",
                "Musiqa inson hissiyotlariga bevosita ta'sir qiluvchi eng kuchli omildir. Xuddi bir xil neytral videotasvir ostiga dramatik qo'rqinchli musiqa qo'yilsa, tomoshabin xavfni his qiladi; agar tantanali quvnoq musiqa qo'yilsa, qahramonlikni ko'radi.",
                "Professional tahlilchi videoni bir marta ovozsiz ko'radi, bir marta faqat ovozini tinglaydi va shundan keyingina har ikki qatlam bir-birini qanday manipulyatsiya qilayotganini tushunadi."
            ]
        ],
        "diagram_type": "stack",
        "diagram_data": {
            "title": "Audiovizual kontent dekonstruksiyasi qatlamlari",
            "subtitle": "Videorolikni 3 ta mustaqil tahliliy komponentga ajratish",
            "layers": [
                {"title": "1-Qatlam: Vizual kadr", "level": "TASVIR", "text": "Rakurs, ranglar, yoritish, imo-ishoralar va kadr ichidagi ramzlar.", "meta": "Ko'z bilan ko'riladigan", "pyramid": False},
                {"title": "2-Qatlam: Matn va nutq", "level": "MATN", "text": "Tanlangan iboralar, mantiqiy xatolar, argumentlar va chaqiriqlar.", "meta": "Mantiqiy mazmun", "pyramid": False},
                {"title": "3-Qatlam: Ovoz va musiqa", "level": "TOVUSH", "text": "Orqa fondagi hissiy musiqa, intonatsiya va qo'shimcha shovqinlar.", "meta": "Hissiy psixologik ta'sir", "pyramid": False}
            ]
        },
        "conclusion": "Media mahsulotini dekonstruksiya qilish o'quvchiga tashqi vizual ta'sirlardan holi holda xolis xulosa chiqarish imkonini beradi.",
        "questions": [
            "Kadr rakursi (burchagi) tomoshabin qabuliga qanday ta'sir o'tkazadi?",
            "Ad Hominem va Soxta dilemma mantiqiy xatolarining mohiyati nimada?",
            "Videorolikni tahlil qilishda ovozsiz ko'rish nima uchun foydali?"
        ]
    },

    # 12-MAVZU
    {
        "num": 12,
        "title": "Media mahsulotini rejalash va ssenariy tuzish",
        "plans": [
            "Media loyihasini g‘oyadan rejalashtirishgacha: mavzu, maqsad va asosiy g‘oya (Logline)",
            "Klassik dramaturgiya qoidalari: uch qismli kompozitsiya va tomoshabin diqqatini boshqarish",
            "Raskadrovka (Storyboard) tuzish: kadrlar ketma-ketligi, vaqt taqsimoti va texnik talablar"
        ],
        "intro": "Sifatli media mahsulot tasodifan yuzaga kelmaydi — uning muvaffaqiyati 80% tayyorgarlik va rejalashtirishga bog'liq. Puxta ishlab chiqilgan ssenariy va raskadrovka ijodiy jamoaning vaqtini, mablag'ini tejaydi hamda g'oyani auditoriyaga aniq yetkazib beradi.",
        "plan_contents": [
            [
                "Har qanday media mahsulot (maqola, videorolik, podkast) yaratish «Logline» — asosiy g'oyani 1-2 ta jumlada ifodalashdan boshlanadi. Agar muallif o'z videosi nima haqida ekanligini 30 soniyada lo'nda tushuntirib bera olmasa, demak g'oya hali to'liq pishmagan.",
                "Rejalashtirish bosqichida auditoriyaning muammosi va ehtiyojlari aniqlanadi: 'Bu video tomoshabinning qaysi savoliga javob beradi?'",
                "Shuningdek, faktlar, statistik raqamlar va intervyu qahramonlari ro'yxati oldindan to'planadi."
            ],
            [
                "Dramaturgiyaning oltin qoidasi — uch qismli strukturadir:",
                "1. Kirish (Ekspozitsiya va ilgak - Hook): Tomoshabinning diqqatini dastlabki soniyalardayoq tortib oluvchi muammo yoki savol o'rtaga tashlanadi.",
                "2. Asosiy qism (Rivojlanish va kulminatsiya): Muammoning turli jihatlari, ziddiyatlar, faktlar va qahramonlar qarashlari bayon etiladi va eng yuqori nuqtaga (kulminatsiyaga) chiqadi.",
                "3. Yechim (Xulosa va Call to Action): Muammoning aniq yechimi, xulosa va tomoshabinni harakatga chaqiruvchi yakun."
            ],
            [
                "Raskadrovka (Storyboard) — bu videorolikning har bir kadrini komikslar kabi qog'ozda yoki raqamli dasturda ketma-ket chizib chiqish jarayonidir.",
                "Har bir kadr ostida uchta muhim ma'lumot yoziladi: kadr rejasi (umumiy, o'rta, yirik plan), kadr davomiyligi (sekundlarda) hamda shu kadrda aytiladigan matn yoki musiqa.",
                "Raskadrovka suratga olish jarayonidagi betartiblikning oldini oladi va montajchiga tayyor yo'l xaritasini beradi."
            ]
        ],
        "diagram_type": "flow",
        "diagram_data": {
            "title": "Media ishlab chiqarish quvuri (Pipeline)",
            "subtitle": "G'oyadan tayyor mahsulotgacha bo'lgan tizimli bosqichlar",
            "steps": [
                {"title": "G'oya va Logline", "text": "Mavzuning dolzarbligini aniqlash va bosh g'oyani shakllantirish.", "tip": "G'oyani 1 gapda ayting", "tag": "01. KONSEPSIYA"},
                {"title": "Ssenariy yozish", "text": "Uch qismli dramaturgik matn va dialoglarni tayyorlash.", "tip": "Kirish, rivoj, yechim", "tag": "02. SSENARIY"},
                {"title": "Raskadrovka", "text": "Kadrlar rejasini chizish va vaqt hisobini (tayming) belgilash.", "tip": "Har bir kadrni rejalang", "tag": "03. STORYBOARD"},
                {"title": "Suratga olish", "text": "Reja asosida sifatli yorug'lik va ovoz bilan tasvirga tushirish.", "tip": "Texnik sifat", "tag": "04. PRODUCTION"}
            ]
        },
        "conclusion": "Mukammal ssenariy va raskadrovka — tomoshabin mehrini qozonuvchi sifatli media mahsulotning tamal toshidir.",
        "questions": [
            "Media mahsulotida 'Logline' tushunchasi nimani anglatadi?",
            "Uch qismli dramaturgiya strukturasining vazifalari nimalardan iborat?",
            "Raskadrovka (Storyboard) suratga olish jarayonini qanday yengillashtiradi?"
        ]
    },

    # 13-MAVZU
    {
        "num": 13,
        "title": "Matn, tasvir va videoni auditoriyaga mos taqdim etish",
        "plans": [
            "Axborotni idrok etish psixologiyasi: matn va vizual vositalar o‘rtasidagi muvozanat",
            "Kross-platformalilik tamoyili: sayt, messenjer va ijtimoiy tarmoqlar uchun format moslashuvi",
            "Tipografika, kontrast va raqamli o‘qiluvchanlik (Readability) standartlari"
        ],
        "intro": "Eng qimmatli axborot ham noo'rin yoki tushunarsiz formatda taqdim etilsa, o'z ta'sir kuchini yo'qotadi. Axborotni qadoqlash san'ati auditoriyaning xususiyatlarini, ularning diqqat ko'lamini va foydalanayotgan qurilmasini chuqur hisobga olishni talab qiladi.",
        "plan_contents": [
            [
                "Tadqiqotlarga ko'ra, inson miyasi vizual tasvirlarni matnga nisbatan o'n minglab marta tezroq idrok qiladi. Biroq faqat rasmlardan iborat kontent chuqur ilmiy fikrni ifodalay olmaydi, faqat quruq matn esa tezda zeriktiradi.",
                "Oltin qoida — axborotning murakkablik darajasiga mos vizual vositalarni tanlashdir: raqamlar va foizlar uchun jadvallar va diagrammalar, jarayonlar uchun bosqichli piktogrammalar, tushunchalar uchun esa qisqa lo'nda ta'riflar berilishi lozim.",
                "Vizual elementlar matnni bezash uchun emas, balki uning ma'nosini ochish va o'qishni yengillashtirish uchun xizmat qilishi shart."
            ],
            [
                "Bir xil axborot barcha platformalarda bir xilda berilmaydi. Kross-platformalilik (Cross-platform) har bir kanalning o'ziga xos qonuniyatlariga bo'ysunadi:",
                "Rasmiy veb-saytlar uchun chuqur tahlil, hujjatli havolalar va to'liq jadvallarga ega uzun maqolalar (longread) mos keladi.",
                "Telegram kanallar uchun 1-2 ta asosiy tezis, infografika va qisqa xulosa beriladi.",
                "Instagram, YouTube Shorts va TikTok uchun esa dastlabki soniyalardanoq dinamik harakat boshlanadigan vertikal video formati tanlanadi."
            ],
            [
                "Raqamli o'qiluvchanlik (Readability) bevosita tipografika qoidalariga tayanadi. Ekranda o'qish qog'ozda o'qishdan farq qiladi — inson ko'zi matnni so'zma-so'z emas, balki «F-shakli» bo'ylab skanerlaydi.",
                "Bitta materialda 2 tadan ortiq shrift turini ishlatmaslik, sarlavha va matn o'rtasidagi ierarxiyani saqlash hamda matn va fon o'rtasida yetarli rang kontrasti bo'lishini ta'minlash shart.",
                "Qatorlar orasidagi masofa (Line height) qulay bo'lsa, o'quvchining ko'zi toliqmaydi va axborotni to'liq qabul qiladi."
            ]
        ],
        "diagram_type": "versus",
        "diagram_data": {
            "title": "Klassik Longread vs Mobil Mikro-kontent",
            "subtitle": "Axborotni taqdim etish formatlarining maqsadli qiyosi",
            "left": {
                "header": "LONGREAD (CHUQUR MAQOLA)",
                "points": [
                    "Batafsil matn, barcha ilmiy tushuntirishlar va formulalar",
                    "Katta tahliliy jadvallar va arxiv manbalari ro'yxati",
                    "Kompyuter monitorida o'tirishga va chuqur mutolaaga mo'ljallangan",
                    "Akademik va tahliliy auditoriya uchun qadrli"
                ],
                "verdict": "Maqsad: Mutaxassis darajasida o'rganish"
            },
            "right": {
                "header": "MIKRO-KONTENT (MOBIL POST)",
                "points": [
                    "3-4 ta asosiy tezis va xulosaviy infografika",
                    "Smartfon ekraniga to'liq sig'adigan vertikal kompozitsiya",
                    "Yo'lda, transportda 1 daqiqada o'qib ketishga qulay",
                    "Keng ommaviy auditoriya uchun tezkor xabardorlik"
                ],
                "verdict": "Maqsad: Tezkor anglash va eslab qolish"
            }
        },
        "conclusion": "Format va auditoriyaning uyg'unligi axborotning manzilga to'liq yetib borishini ta'minlovchi asosiy ko'prikdir.",
        "questions": [
            "F-shaklidagi o'qish modeli (F-pattern) nimani anglatadi?",
            "Kross-platforma tamoyili nima sababdan axborot tarqatishda muhim?",
            "Tipografikada kontrast yetishmovchiligi qanday salbiy oqibatlarga olib keladi?"
        ]
    },

    # 14-MAVZU
    {
        "num": 14,
        "title": "Matnli va tasvirli kontent yoki infografika yaratish",
        "plans": [
            "Infografika turlari: statistik, xronologik, taqqoslovchi va jarayonli infografikalar",
            "Vizual ierarxiya, ranglar uyg‘unligi va ma'lumotlarni ixchamlashtirish qoidalari",
            "Zamonaviy grafik vositalar (Canva, Figma) va axborotning ilmiy haqqoniyligi"
        ],
        "intro": "Zamonaviy raqamli axborot makonida infografika eng ommabop va talabgir didaktik vositalardan biridir. U quruq raqamlar va murakkab hisobotlarni har qanday o'quvchi uchun tushunarli, ko'rgazmali va jozibador ko'rinishga keltiradi.",
        "plan_contents": [
            [
                "Infografikalar maqsadiga ko'ra bir necha turlarga bo'linadi:",
                "1. Statistik infografika — raqamlar, ulushlar va dinamikani ko'rsatishga xizmat qiladi (diagrammalar, piktogrammalar).",
                "2. Jarayonli infografika (Flowchart) — harakatlarning qadamma-qadam ketma-ketligini tushuntiradi (masalan: qonun qabul qilinish bosqichlari).",
                "3. Qiyosiy infografika — ikki yoki undan ortiq tushunchalarning kuchli va zaif tomonlarini jadval shaklida solishtiradi.",
                "4. Geografik infografika — ma'lumotlarni xaritalar va hududlar kesimida taqsimlaydi."
            ],
            [
                "Infografika yaratishning bosh tamoyili — ma'lumotlarni shafqatsizlarcha qisqartirishdir. Matnning 70 foizidan voz kechib, faqatgina ma'no tashuvchi eng zarur kalit jumlalar va raqamlargina qoldiriladi.",
                "Ranglar palitrasi 3 tadan oshmasligi lozim: 60% asosiy neytral fon, 30% strukturali bloklar va 10% diqqatni tortuvchi yorqin aksent rangi.",
                "Agar dizayner hamma narsani yorqin rangga bo'yab tashlasa, vizual shovqin hosil bo'ladi va o'quvchi qayerga qarashni bilmay qoladi."
            ],
            [
                "Bugungi kunda Figma, Canva, Piktochart kabi qulay grafik platformalar har bir mutaxassisga dizaynerlik ta'limisiz ham professional plakatlar yasash imkonini beradi.",
                "Biroq chiroyli dizayn hech qachon xato ma'lumotni oqlay olmaydi. Har qanday infografikaning eng pastki qismida ma'lumot olingan rasmiy manba va uning sanasi kichik harflar bilan ko'rsatilishi shart.",
                "Manbasi ko'rsatilmagan chiroyli grafika feyk xabarning xavfli ko'rinishlaridan biri bo'lishi mumkin."
            ]
        ],
        "diagram_type": "grid",
        "diagram_data": {
            "title": "Infografikaning 4 ta asosiy turi va qo'llanishi",
            "subtitle": "Vazifaga mos to'g'ri vizual modelni tanlash matrisi",
            "quadrants": [
                {"title": "Statistik Infografika", "badge": "RAQAMLAR", "text": "Diagrammalar, foizlar va o'sish ko'rsatkichlarini vizuallashtirish.", "insight": "Grafiklar mutanosib bo'lsin"},
                {"title": "Jarayonli (Flowchart)", "badge": "BOSQICHLAR", "text": "Bosqichma-bosqich harakatlar algoritmi va yo'l xaritalari.", "insight": "Yo'nalish strelkalarini qo'ying"},
                {"title": "Qiyosiy Infografika", "badge": "SOLISHTIRUV", "text": "Ikki hodisa yoki mahsulotning afzallik va kamchiliklari tahlili.", "insight": "Aniq mezonlar asosida"},
                {"title": "Xronologik (Timeline)", "badge": "TARIX", "text": "Voqealar va tarixiy bosqichlarning vaqt shkalasi bo'yicha ketma-ketligi.", "insight": "Sanalar aniqligi shart"}
            ]
        },
        "conclusion": "Yaxshi infografika — bu matnsiz ham asosiy ma'noni tomoshabinga to'liq yetkazib bera oladigan san'atdir.",
        "questions": [
            "Infografikaning qanday asosiy turlari mavjud?",
            "Vizual rang balansidagi 60-30-10 qoidasi nimani anglatadi?",
            "Infografika tagida manbani ko'rsatish nima sababdan qat'iy talab sanaladi?"
        ]
    },

    # 15-MAVZU
    {
        "num": 15,
        "title": "Qisqa videoxabar tayyorlash va taqdim etish",
        "plans": [
            "Dastlabki 3 soniya qoidasi (Hook): e'tiborni ushlash va dinamik montaj asoslari",
            "Mobil video ishlab chiqarish texnikasi: petlichka mikrofoni, yoritish va kadr burchagi",
            "Subtitrlar, dinamik titrlar va videoni platformalarga eksport qilish mezonlari"
        ],
        "intro": "Video bugungi kunda internetdagi barcha trafikning 80 foizdan ortig'ini tashkil qilmoqda. Ayniqsa, vertikal qisqa videoroliklar (Reels, Shorts, TikTok) axborot yetkazishning eng ommaviy vositasiga aylandi. Ushbu darsda sifatli videoxabar yaratishning amaliy sirlari ochiladi.",
        "plan_contents": [
            [
                "Mobil ekranda tomoshabin barmoq harakati bilan videoni o'tkazib yuborishiga bor-yo'g'i 1-3 soniya kerak bo'ladi. Shu bois 'Dastlabki 3 soniya' (Hook - Ilgak) butun rolik taqdirini hal qiladi.",
                "Videoni aslo uzoq salomlashish, o'zini tanishtirish yoki jimlik bilan boshlash mumkin emas. Birinchi soniyadayoq qiziqarli savol berilishi, kutilmagan harakat ko'rsatilishi yoki muammoning o'tkir nuqtasi e'lon qilinishi shart.",
                "Kadr har 3-5 soniyada yangilanib turishi (plan almashishi, rasm yoki matn chiqib kelishi) tomoshabinning diqqatini ushlab turishga xizmat qiladi."
            ],
            [
                "Ko'pchilik havaskorlar videoning faqat tasviriga e'tibor berib, ovoz sifatini unutadilar. Aslida tomoshabin biroz xira tasvirni kechirishi mumkin, ammo shovqinli, aks-sado berayotgan yoki tushunarsiz ovozli rolikni darhol o'chiradi.",
                "Oddiy simli yoki simsiz petlichka mikrofonidan foydalanish ovoz tozaligini bir necha barobar oshiradi.",
                "Yorug'lik manbai (deraza yoki chiroq) qahramonning orqasida emas, balki old tomonida, yuziga 45 daraja burchak ostida tushishi kerak. Orqa fondan tushgan yorug'lik qahramonni qora siluetga aylantirib qo'yadi."
            ],
            [
                "Ijtimoiy tarmoq foydalanuvchilarining 75% dan ortig'i videolarni ovozsiz (masalan, jamoat transportida, ishda yoki darsda) tomosha qiladi. Shu bois videoga subtitr joylashtirish ixtiyoriy emas, balki majburiy shartdir.",
                "CapCut, VN yoki Premiere kabi dasturlar orqali avtomatik subtitrlar yaratish va muhim so'zlarni yorqin ranglar bilan ajratib ko'rsatish mumkin.",
                "Rolik oxirida albatta aniq Call to Action (fikr bildirishga chorlov yoki kanalga ulanish taklifi) bo'lishi lozim."
            ]
        ],
        "diagram_type": "inspector",
        "diagram_data": {
            "title": "Samarali mobil video anatomiyasi",
            "subtitle": "Qisqa rolikni professional darajada suratga olish mezonlari",
            "items": [
                {"title": "0-3 soniya: Ilgak", "badge": "HOOK", "desc": "Diqqatni ushlovchi o'tkir savol yoki kutilmagan vizual harakat.", "action": "Sekin boshlamang"},
                {"title": "Ovoz Tozaligi", "badge": "AUDIO", "desc": "Petlichka mikrofonidan foydalanish, tashqi shovqinlarni tozalash.", "action": "Ovoz sifatini tekshiring"},
                {"title": "To'g'ri Yorug'lik", "badge": "YORUQLIK", "desc": "Yorug'lik qahramonning yuziga tushishi, qora siluetdan qochish.", "action": "Chiroqqa qarab turing"},
                {"title": "Aniq Subtitrlar", "badge": "TITRLAR", "desc": "Tovushsiz tomosha qiluvchilar uchun ekranda dinamik matn.", "action": "Subtitrlarni yoqing"}
            ]
        },
        "conclusion": "Ovoz, yorug'lik va dastlabki ilgak uyg'unlashgan qisqa video xabar eng murakkab fikrni ham millionlarga yetkaza oladi.",
        "questions": [
            "Videoda dastlabki 3 soniya (Hook) nima sababdan hal qiluvchi rol o'ynaydi?",
            "Ovoz sifati nima uchun tasvir sifatidan muhimroq sanaladi?",
            "Subtitrlar nima uchun barcha ijtimoiy roliklarga qo'yilishi shart?"
        ]
    },

    # 16-MAVZU
    {
        "num": 16,
        "title": "Media etikasi va netiket",
        "plans": [
            "Netiket tushunchasi: tarmoqda o‘zini tutish madaniyati, yozish odobi va raqamli hurmat",
            "Jurnalistika etikasi tamoyillari: xolislik, shaxsiy daxlsizlik va faktlarni buzmaslik",
            "Raddiya (retraction) berish madaniyati va xatolar ustida halol ishlash"
        ],
        "intro": "Virtual maydondagi erkinlik boshqalarning qadr-qimmati va sha'nini toptash huquqini bermaydi. Har qanday sivilizatsiyalashgan jamiyat kabi raqamli olam ham o'zining yozilmagan odob-axloq qoidalariga — netiketga tayanadi.",
        "plan_contents": [
            [
                "Netiket (Network Etiquette — tarmoq etikasi) — internetda, guruhlarda, elektron pochtada va ijtimoiy tarmoqlarda muloqot qilishning qabul qilingan odob qoidalari majmuidir.",
                "Katta harflar (CAPS LOCK) bilan yozish internet madaniyatida baqirish, janjallashish va agressiya deb qabul qilinadi. Birovning ruxsatisiz shaxsiy yozishmalarni (skrinshotlarni) ommaviy guruhlarga chiqarish ishonchni suiiste'mol qilish va noaxloqiylikdir.",
                "Shuningdek, tematik guruhlarda mavzudan tashqari (off-topic) reklama yoki zanjirli tabriknomalar tarqatish boshqalarning vaqtiga hurmatsizlik sanaladi."
            ],
            [
                "Kasbiy media etikasining asosiy kodeksiga to'rt tamoyil kiradi: Haqiqatga intilish, zararni minimallashtirish, mustaqillik va mas'uliyat.",
                "Insonning fojiasi, baxtsiz hodisasi yoki shaxsiy yo'qotishlarini shov-shuvli kontentga aylantirish, jasad yoki jabrlanuvchi tasvirini pikselsiz ochiq ko'rsatish kasb etikasiga ziddir.",
                "Ayniqsa, voyaga yetmagan bolalar va jinoyatda hali sud tomonidan aybi isbotlanmagan gumonlanuvchilarning yuzini va shaxsini ochiqlamaslik qonuniy va axloqiy talabdir."
            ],
            [
                "Har bir inson xato qilishi mumkin. Ammo tarmoqda noto'g'ri xabarni tarqatib qo'ygach, uni jimgina o'chirib yuborish mas'uliyatsizlikdir.",
                "Haqiqiy axloqiy yondashuv — rasmiy raddiya (retraction) e'lon qilishdir. Muallif o'z sahifasida qaysi xabar xato bo'lgani, aslida haqiqat nimaligini yozib, auditoriyadan xolis uzr so'rashi kerak.",
                "Raddiya berish muallifning obro'sini tushirmaydi, aksincha uning o'z so'ziga javob bera oladigan halol inson ekanligini ko'rsatadi."
            ]
        ],
        "diagram_type": "versus",
        "diagram_data": {
            "title": "Madaniyatli Netiket vs Tarmoqdagi Toksiklik",
            "subtitle": "Raqamli muloqotdagi to'g'ri va noto'g'ri xatti-harakatlar qiyosi",
            "left": {
                "header": "MADANIYATLI NETIKET (TO'G'RI)",
                "points": [
                    "Boshqalar fikriga hurmat bilan yondashish va xolis bahslashish",
                    "Kichik harflar bilan, savodli va bitta xabarga jamlab yozish",
                    "Shaxsiy yozishmalar va fotosuratlarni ruxsat bilan ulashish",
                    "Xatoni tan olish va zarur bo'lganda ochiq raddiya berish"
                ],
                "verdict": "Natija: Sog'lom va nufuzli obro'-e'tibor"
            },
            "right": {
                "header": "TOKSIK VA AGRESSIV MUHIT (NOTO'G'RI)",
                "points": [
                    "CAPS LOCK bilan baqirib yozish va haqoratomuz laqablar qo'yish",
                    "Birovning shaxsiy ma'lumotlarini ruxsatsiz guruhlarga sizdirish",
                    "Spam, offtopik va tasdiqlanmagan mish-mishlarni tarqatish",
                    "Xatoni yashirish yoki dalilsiz ayblovlarni davom ettirish"
                ],
                "verdict": "Natija: Bloklanish va qonuniy javobgarlik"
            }
        },
        "conclusion": "Tarmoq etikasi har bir insonning o'zidan boshlanadi va virtual dunyoni xavfsiz maskanga aylantiradi.",
        "questions": [
            "Netiket tushunchasi nimani anglatadi va nima uchun CAPS LOCK yozish taqiqlanadi?",
            "Media etikasida zararni minimallashtirish tamoyili qanday ishlaydi?",
            "Raddiya (retraction) nima va u professional OAVda qanday e'lon qilinadi?"
        ]
    },

    # 17-MAVZU
    {
        "num": 17,
        "title": "Mualliflik huquqi, plagiat va manba ko‘rsatish",
        "plans": [
            "Intellektual mulk va mualliflik huquqi (Copyright) to‘g‘risidagi qonunchilik asoslari",
            "Plagiatning turlari: to‘g‘ridan-to‘g‘ri o‘g‘irlik, parafrazli va o‘z-o‘zini ko‘chirish",
            "Creative Commons litsenziyalari, Fair Use (Halol foydalanish) va jamoat mulki (Public Domain)"
        ],
        "intro": "Intellektual mehnat — bu inson ijodiy va aqliy faoliyatining eng oliy mahsulidir. Birovning matni, surati, musiqasi yoki kodidan ruxsatsiz foydalanish oddiy moddiy o'g'irlik bilan tenglashtiriladi. Ushbu darsda qonuniy foydalanish mezonlari o'rganiladi.",
        "plan_contents": [
            [
                "O'zbekiston Respublikasining 'Mualliflik huquqi va turdosh huquqlar to'g'risida'gi Qonuniga muvofiq, asar yaratilgan paytdan boshlab mualliflik huquqi avtomatik vujudga keladi. Buning uchun maxsus ro'yxatdan o'tish yoki belgi qo'yish shart emas.",
                "Muallif shaxsiy nomulkiy huquqlarga (asar muallifi deb e'tirof etilish huquqi) va mulkiy huquqlarga (asarni ko'paytirish, tarqatish, sotishdan daromad olish huquqi) ega.",
                "Muallifning ruxsatisiz uning asarini tijoriy loyihalarda ishlatish, o'zgartirish yoki o'z nomidan e'lon qilish qonuniy javobgarlikka sabab bo'ladi."
            ],
            [
                "Plagiat — bu birovning intellektual mehnati natijalarini o'zlashtirib olishdir. U uch xil shaklda namoyon bo'ladi:",
                "To'g'ridan-to'g'ri plagiat: Matn, jadval yoki ilmiy formulani so'zma-so'z ko'chirib, muallif nomini umuman tilga olmaslik.",
                "Parafrazli plagiat: Birovning fikrini boshqa so'zlar bilan ifodalab (sinonimlar bilan almashtirib), lekin g'oya kimga tegishli ekanligiga havola bermaslik.",
                "O'z-o'zini plagiat qilish (Self-plagiarism): Avval boshqa joyda chop etilgan o'zining eski ishini yangi ilmiy asar sifatida qayta topshirish."
            ],
            [
                "Internetdagi barcha narsa ham taqiqlangan emas. Mualliflar o'z asarlarini keng ommaga taqdim etish uchun maxsus litsenziyalardan foydalanadilar:",
                "Creative Commons (CC) — muallif o'z asaridan bepul foydalanish shartlarini (masalan, CC-BY — faqat muallif nomini ko'rsatish sharti bilan) belgilab beradi.",
                "Public Domain (Jamoat mulki) — muallif vafotidan so'ng qonunda belgilangan muddat (odatda 70 yil) o'tgan yoki muallif o'z ixtiyori bilan huquqdan voz kechgan asarlardir. Ulardan hamma erkin foydalanishi mumkin.",
                "Fair Use (Halol foydalanish) — ta'lim, tanqidiy sharh yoki ilmiy tahlil maqsadida asardan qisqa iqtibos olishga ruxsat beruvchi me'yordir."
            ]
        ],
        "diagram_type": "stack",
        "diagram_data": {
            "title": "Mualliflik huquqi va erkin foydalanish pog'onalari",
            "subtitle": "Qat'iy cheklovdan to'liq erkin jamoat mulkigacha bo'lgan litsenziyalar",
            "layers": [
                {"title": "Barcha huquqlar himoyalangan", "level": "QAT'IY", "text": "Faqat muallifning yozma ruxsati yoki sotib olingan litsenziya bilan ishlatiladi.", "meta": "Ruxsatsiz taqiqlanadi", "pyramid": False},
                {"title": "Fair Use (Iqtibos olish)", "level": "CHEKLANGAN", "text": "Ilmiy, ta'limiy va tanqidiy maqsadlarda qisqa hajmda, manbani ko'rsatib ishlatish.", "meta": "Ta'lim maqsadida", "pyramid": False},
                {"title": "Creative Commons (CC)", "level": "OCHIQ", "text": "Muallif nomini ko'rsatgan holda bepul nusxalash va tarqatishga ruxsat.", "meta": "CC-BY litsenziyasi", "pyramid": False},
                {"title": "Public Domain (Ochiq mulk)", "level": "ERKIN", "text": "Huquq muddati tugagan klassik asarlar, xalq og'zaki ijodi va davlat ramzlari.", "meta": "Cheklovlarsiz", "pyramid": False}
            ]
        },
        "conclusion": "Birovning mehnati va mualliflik huquqini hurmat qilish — intellektual halollikning bosh mezonidir.",
        "questions": [
            "Mualliflik huquqi asar ustida qachon vujudga keladi?",
            "Plagiatning qanday turlari mavjud va parafrazli plagiat nima?",
            "Creative Commons (CC) litsenziyasi foydalanuvchiga qanday qonuniy imkoniyat beradi?"
        ]
    },

    # 18-MAVZU
    {
        "num": 18,
        "title": "Shaxsiy ma’lumotlar, raqamli gigiyena va kiberbulling",
        "plans": [
            "Raqamli iz (Digital footprint) va shaxsga doir ma'lumotlarning daxlsizligi",
            "Raqamli gigiyena qoidalari: kuchli parollar, ikki bosqichli autentifikatsiya (2FA) va fishing",
            "Kiberbulling, onlayn ta'qib va ularga qarshi huquqiy hamda psixologik himoya choralari"
        ],
        "intro": "Bugungi kunda har birimiz virtual makonda o'zimizning raqamli egizagimizga egamiz. Biz qoldirayotgan har bir iz, har bir qidiruv so'rovi va yuklangan fotosurat kelajakda xavfsizligimiz, karyeramiz va shaxsiy hayotimizga bevosita ta'sir ko'rsatishi mumkin.",
        "plan_contents": [
            [
                "Raqamli iz (Digital footprint) — bu inson internetdan foydalanganda o'zidan qoldiradigan barcha ma'lumotlar tarixidir. U faol (biz bilib yuklagan postlar, sharhlar) va nofaol (saytlar tomonidan yig'ilgan cookie fayllar, IP-manzil, geolokatsiya) turlarga bo'linadi.",
                "O'zbekiston Respublikasining 'Shaxsga doir ma'lumotlar to'g'risida'gi Qonuniga ko'ra, insonning pasport ma'lumotlari, yashash manzili, biometrik ma'lumotlari, bank kartalari va telefon raqamlari maxsus himoyalanadi.",
                "Ushbu ma'lumotlarni ochiq guruhlarda qoldirish yoki shubhali botlarga kiritish kiberfiribgarlarga qulay imkoniyat yaratadi."
            ],
            [
                "Raqamli gigiyena — bu xuddi kundalik tozalik kabi virtual olamdagi xavfsizlik odatlaridir. Uning oltin qoidalari:",
                "Parol xavfsizligi: Kamida 12 ta belgidan iborat, katta va kichik harflar, raqamlar va belgilarni o'z ichiga olgan murakkab parollardan foydalanish. Bitta parolni hamma akkauntlarga qo'ymaslik.",
                "Ikki bosqichli autentifikatsiya (2FA): Paroldan tashqari telefon raqamiga keladigan SMS yoki Authenticator ilovasi kodini talab qiluvchi tizimni barcha messenjer va pochtalarda yoqish.",
                "Fishing (Phishing) tuzoqlaridan ogoh bo'lish: Rasmiy bank yoki kompaniya nomidan kelgan soxta havolalarni ochmaslik va ularga kartaning maxfiy kodini aslo kiritmaslik."
            ],
            [
                "Kiberbulling — tarmoqda insonni tizimli ravishda haqoratlash, kamsitish, qo'rqitish yoki unga nisbatan ommaviy nafrat kampaniyasini uyushtirishdir.",
                "Onlayn agressiyaga uchragan inson uchun eng to'g'ri qadamlar: tajovuzkor bilan janjallashmaslik, barcha haqoratomuz yozishmalarning skrinshotlarini dalil sifatida saqlab olish, foydalanuvchini bloklash va platforma ma'muriyatiga shikoyat yuborish.",
                "Zarur hollarda shaxsiy sha'n va qadr-qimmatni himoya qilish uchun huquqni muhofaza qiluvchi organlarga murojaat qilish qonuniy huquqdir."
            ]
        ],
        "diagram_type": "grid",
        "diagram_data": {
            "title": "Raqamli xavfsizlik va gigiyena qalqoni",
            "subtitle": "Kiberxavflardan himoyalanishning 4 ta mustahkam ustuni",
            "quadrants": [
                {"title": "2FA Autentifikatsiya", "badge": "HIMOYA", "text": "Barcha Telegram, Google va ijtimoiy tarmoqlarda ikki bosqichli tasdiqni yoqish.", "insight": "Parolning o'zi yetarli emas"},
                {"title": "Raqamli Iz Nazorati", "badge": "MAXFIYLIK", "text": "Pasport, bank kartasi va uy manzilini begonalarga aslo bermaslik.", "insight": "Geolokatsiyani o'chiring"},
                {"title": "Fishingga Qarshi Filtr", "badge": "OGOHLIK", "text": "«Yutuq chiqdi», «Yordam bering» kabi shubhali havolalarni bosmaslik.", "insight": "Sayt domenini tekshiring"},
                {"title": "Kiberbullingga Cheklov", "badge": "TO'SIQ", "text": "Agressorlar bilan bahslashmasdan, dalillarni saqlab 'Blok' qilish.", "insight": "Skrinshotlarni saqlang"}
            ]
        },
        "conclusion": "Raqamli gigiyena qoidalariga rioya qilish virtual makonda erkin va xavfsiz harakatlanish garovidir.",
        "questions": [
            "Raqamli iz (Digital footprint) nima va u inson hayotiga qanday ta'sir qiladi?",
            "Ikki bosqichli autentifikatsiya (2FA) akkauntni qanday himoya qiladi?",
            "Fishing xabarlarining asosiy xarakterli belgilari qaysilar?"
        ]
    },

    # 19-MAVZU
    {
        "num": 19,
        "title": "Media mahsulotiga iqtibos va manba havolalarini to‘g‘ri kiritish",
        "plans": [
            "Iqtibos keltirish madaniyati: to‘g‘ridan-to‘g‘ri va bilvosita (parafraz) iqtiboslar",
            "Xalqaro iqtibos standartlari: APA, MLA, Chikago va milliy bibliografik mezonlar",
            "Raqamli matnlarda giperhavolalar (hyperlinks) va manbalar ro‘yxatini shakllantirish"
        ],
        "intro": "Ilmiy, tahliliy yoki ta'limiy media kontent yaratishda boshqalarning tadqiqotlariga tayanish tabiiy jarayondir. Biroq ushbu ma'lumotlarni to'g'ri rasmiylashtirish va ularga xalqaro standartlar asosida havola berish akademik savodxonlikning muhim shartidir.",
        "plan_contents": [
            [
                "Iqtibos keltirishning ikki asosiy usuli mavjud:",
                "1. To'g'ridan-to'g'ri iqtibos: Muallifning so'zlari hech qanday o'zgarishsiz, asl holicha qo'shtirnoq ichida beriladi. Qavsda esa muallifning familiyasi, asar chiqqan yil va aynan qaysi sahifadan olingani ko'rsatiladi. Masalan: «...» (Taylaqov, 2021, 45-b.).",
                "2. Bilvosita iqtibos (parafraz): Muallifning fikri o'z so'zlaringiz bilan umumlashtirib bayon etiladi. Bu holatda qo'shtirnoq qo'yilmaydi, ammo g'oya egasiga havola berish baribir shart sanaladi."
            ],
            [
                "Dunyo ilm-fanida bir necha yetakchi iqtibos standartlari qo'llaniladi:",
                "APA (American Psychological Association) — ijtimoiy va ta'limiy fanlarda eng keng tarqalgan standart bo'lib, 'Muallif-sana' modeliga tayanadi.",
                "MLA (Modern Language Association) — adabiyot va gumanitar fanlarda qo'llaniladi va sahifa raqamiga urg'u beradi.",
                "O'zbekistonda OAK (Oliy attestatsiya komissiyasi) tomonidan tasdiqlangan davlat bibliografik standarti (GOST 7.1) amal qiladi, unda asarning barcha nashriyot rekvizitlari to'liq ko'rsatilishi talab etiladi."
            ],
            [
                "Raqamli OAV va onlayn nashriyotlarda havolalar gipermatn (giperhavola) orqali kiritiladi. Bu o'quvchiga matnni o'qiyotgan joyning o'zida bir marta bosish orqali asl hujjatga o'tish imkonini beradi.",
                "Giperhavola qo'yishda 'bu yerga bosing' deb yozish yomon amaliyot hisoblanadi. Havola aynan ma'no tashuvchi termin yoki tashkilot nomiga biriktirilishi lozim (Masalan: '...YUNESKO hisobotida qayd etilganidek...').",
                "Maqola so'ngida foydalanilgan barcha adabiyotlar va veb-manbalar alifbo tartibida to'liq ro'yxat shaklida keltirilishi shart."
            ]
        ],
        "diagram_type": "flow",
        "diagram_data": {
            "title": "Iqtibos va havolalarni rasmiylashtirish algoritmi",
            "subtitle": "Boshqa muallif fikrini o'z ishiga qonuniy va to'g'ri kiritish",
            "steps": [
                {"title": "Fikrni tanlash", "text": "Mavzuga mos zaruriy fikr yoki statistikani ajratib olish.", "tip": "Kontekstni buzmang", "tag": "01. TANLOV"},
                {"title": "Formatni belgilash", "text": "So'zma-so'z («...») yoki parafraz (o'z so'zingiz bilan) usulini tanlash.", "tip": "To'g'ridan-to'g'ri yoki bilvosita", "tag": "02. FORMAT"},
                {"title": "Standart kiritish", "text": "APA yoki milliy standart bo'yicha muallif va yilni ko'rsatish.", "tip": "Rekvizitlarni yozing", "tag": "03. STANDART"},
                {"title": "Adabiyotlar ro'yxati", "text": "Maqola so'ngida to'liq manzil va faol giperhavolani joylash.", "tip": "Alifbo tartibida", "tag": "04. RO'YXAT"}
            ]
        },
        "conclusion": "To'g'ri rasmiylashtirilgan iqtiboslar muallif fikrlarini mustahkamlaydi va uning ilmiy savodxonligini yuksaltiradi.",
        "questions": [
            "To'g'ridan-to'g'ri va bilvosita (parafraz) iqtibosning farqlari nimada?",
            "APA iqtibos standartining asosiy xususiyati qanday?",
            "Raqamli matnlarda giperhavola qanday to'g'ri biriktiriladi?"
        ]
    },

    # 20-MAVZU
    {
        "num": 20,
        "title": "Maxfiylik va onlayn muloqotga oid vaziyatlarni yechish",
        "plans": [
            "Real hayotiy keyslar (Case study): dezinformatsiya va moliyaviy fishing xurujlarini bartaraf etish",
            "Ruxsatsiz shaxsiy tasvirlar tarqalishi va shaxsiy daxlsizlik buzilishida huquqiy algoritmlar",
            "Media savodxonlik madaniyatini amaliyotga tatbiq etish: xavfsiz va mas'uliyatli raqamli fuqarolik"
        ],
        "intro": "Mazkur yakuniy mavzuda butun kurs davomida egallangan nazariy bilimlar, tahliliy usullar va xavfsizlik mezonlari real amaliy vaziyatlar hamda muammoli keyslar (Case study) asosida umumlashtiriladi.",
        "plan_contents": [
            [
                "1-Keys: Bank kartalari va fishing xuruji.",
                "Vaziyat: Telegram orqali go'yoki davlat idorasi nomidan «Barcha fuqarolarga 1 million so'm moddiy yordam tarqatilmoqda, pulni olish uchun kartangiz raqami va SMS kodini kiriting» degan xabar keldi.",
                "Yechish algoritmi: Havolaning domenini tekshirish (rasmiy .gov.uz emasligi aniqlanadi). Shoshilinch bosim va tekin pul va'dasi feyk belgisidir. Foydalanuvchi havolani ochmasdan, xabarni spam deb belgilaydi va guruh a'zolarini ogohlantiradi."
            ],
            [
                "2-Keys: Ruxsatsiz fotosuratlarning tarqalishi.",
                "Vaziyat: Talabaning shaxsiy fotosurati tanishlari tomonidan hazil sifatida mem qilinib, ruxsatisiz jamoat kanaliga joylandi.",
                "Yechish algoritmi: 1) Emotsiyalarga berilmasdan, postning havolasi va skrinshotini saqlab olish. 2) Kanal ma'muriyatiga O'zbekiston Respublikasining 'Shaxsga doir ma'lumotlar to'g'risida'gi Qonunini eslatgan holda xabarni darhol o'chirishni talab qilish. 3) Rad etilgan taqdirda platforma qo'llab-quvvatlash xizmatiga (Report) murojaat qilish."
            ],
            [
                "Mas'uliyatli raqamli fuqarolik — bu nafaqat o'zini himoya qilish, balki butun internet muhitini toza va sog'lom saqlashga hissa qo'shishdir.",
                "Savodli inson o'zi tekshirmagan xabarni aslo yaqinlariga ulashmaydi, kiberbullingga sherik bo'lmaydi, intellektual mulkdan qonuniy foydalanadi va har qanday axborotga sog'lom tanqidiy aql bilan qaraydi.",
                "Media savodxonlik bir martalik dars emas, balki axborot asrida ongli va erkin yashashning doimiy hayot tarzidir."
            ]
        ],
        "diagram_type": "grid",
        "diagram_data": {
            "title": "Muammoli vaziyatlarni hal qilish amaliy algoritmi",
            "subtitle": "Inqirozli vaziyatlarda to'g'ri qadamlar ketma-ketligi",
            "quadrants": [
                {"title": "1-Qadam: Sovuqqonlik", "badge": "TAHLIL", "text": "Hissiyotga berilmaslik, vahima qilmaslik va vaziyatni xolis baholash.", "insight": "Shoshilmasdan o'ylang"},
                {"title": "2-Qadam: Dalil To'plash", "badge": "SKRINSHOT", "text": "Postlar, xabarlar, havolalar va sanalarning skrinshotlarini saqlash.", "insight": "Faktlarni hujjatlashtiring"},
                {"title": "3-Qadam: Huquqiy Asos", "badge": "QONUN", "text": "Shaxsga doir ma'lumotlar va mualliflik huquqi qonunlariga tayanish.", "insight": "Qonuniy talab qo'ying"},
                {"title": "4-Qadam: Xavfsiz Harakat", "badge": "HIMOYA", "text": "Akkauntni 2FA bilan kuchaytirish, hisobot berish (Report) va bloklash.", "insight": "Tizimli chora ko'ring"}
            ]
        },
        "conclusion": "Media va axborot savodxonligini hayotga tatbiq etish har bir insonning o'z taqdiriga ongli egalik qilishining kafolatidir.",
        "questions": [
            "Fishing xabarlari orqali keladigan xavflarni qanday bartaraf etish mumkin?",
            "Shaxsiy fotosuratlar ruxsatsiz tarqalganda qanday amaliy va huquqiy qadamlar tashlanadi?",
            "Mas'uliyatli raqamli fuqaro qanday fazilatlarga ega bo'lishi kerak?"
        ]
    }
]

ALL_20_RICH_TOPICS = RICH_TOPICS + MORE_RICH_TOPICS
print(f"Total Rich Topics loaded: {len(ALL_20_RICH_TOPICS)}")

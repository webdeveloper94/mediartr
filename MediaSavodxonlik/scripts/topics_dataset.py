# -*- coding: utf-8 -*-
"""
Full lecture dataset for 20 topics of Media va Axborot Savodxonligi.
"""

TOPICS_DATA = [
    {
        "num": 1,
        "title": "Media va axborot savodxonligi asoslari",
        "intro": "Bugungi axborotlashgan jamiyatda har bir inson kuniga yuzlab xabarlar, yangiliklar va vizual kontentlar oqimiga duch keladi. Media va axborot savodxonligi (MAS) — bu axborotni shunchaki passiv qabul qiluvchi bo'lishdan ongli va tanqidiy tahlil qiluvchi faol foydalanuvchiga aylanish madaniyatidir.",
        "sections": [
            {
                "title": "1. Media va axborot tushunchasining mazmun-mohiyati",
                "paragraphs": [
                    "Media (lotincha «medium» — vosita, oraliq vositachi) — axborotni jamiyatga yetkazib berish, tarqatish va saqlash uchun xizmat qiladigan texnik va aloqa kanallari tizimidir.",
                    "Axborot savodxonligi esa insonning axborotga bo‘lgan ehtiyojini aniqlash, ishonchli manbalarni topish, ularni samarali qayta ishlash hamda kundalik hayot va kasbiy faoliyatda to‘g‘ri qo‘llash qobiliyatidir. YUNESKO ta'rifiga ko'ra, axborot savodxonligi insonning butun umri davomida o'z-o'zini rivojlantirishining asosi hisoblanadi."
                ]
            },
            {
                "title": "2. Axborotning inson va jamiyat hayotidagi o'rni",
                "paragraphs": [
                    "Zamonaviy dunyoda axborot strategik resursga aylandi. U iqtisodiy, siyosiy va shaxsiy qarorlar qabul qilishda yetakchi omil bo‘lib xizmat qiladi. Biroq axborotning haddan tashqari ko'pligi «axborot to'lib-toshishi» (information overload) va chalg'ish sindromini keltirib chiqaradi.",
                    "Shu bois axborotni shunchaki iste'mol qilish emas, balki uni tahlil qilish, asosli dalillarga tayanadigan xulosalar chiqarish har bir kasb egasi uchun zaruriy malakadir."
                ]
            },
            {
                "title": "3. Fakt va shaxsiy fikr (talqin) chegarasi",
                "paragraphs": [
                    "Media savodxonlikning birlamchi mezoni — xabardagi faktlarni muallifning shaxsiy talqini yoki hissiy qarashlaridan ajrata olishdir.",
                    "Fakt — bu real mavjud bo'lgan, sanasi, o'lchami, hujjati yoki videosi bilan isbotlanishi mumkin bo'lgan hodisadir. Fikr esa subyektiv bo'lib, insonning shaxsiy bahosi, his-tuyg'usi yoki taxminidir."
                ]
            }
        ],
        "diagram": {
            "title": "Media va axborotni tahlil qilish modeli",
            "subtitle": "Xabarni qabul qilishdan ongli qaror qabul qilishgacha bo'lgan tizim",
            "blocks": [
                {"title": "Kuzatish va qabul", "text": "Xabarni his-tuyg'usiz o'qish, birlamchi fakt va uning ko'lamini aniqlash.", "insight": "Vahimali sarlavhaga aldanmang", "step": True},
                {"title": "Fakt va talqin", "text": "Hujjatli dalillarni muallifning subyektiv qarashlaridan ajratish.", "insight": "Fakt o'lchanadi, fikr baholanadi", "step": True},
                {"title": "Ongli xulosa", "text": "Rasmiy ma'lumotlar bilan solishtirib, mustaqil to'g'ri xulosa chiqarish.", "insight": "Birlamchi manbaga tayaning", "step": True}
            ]
        },
        "conclusion": "Media savodxonlik — bu bir martalik ko'nikma emas, balki axborot asrida yashashning doimiy aqliy filtri va madaniyatidir.",
        "questions": [
            "Media va axborot savodxonligi shaxsga nima beradi?",
            "Fakt bilan shaxsiy fikr o'rtasidagi asosiy farqlar nimada?",
            "Axborot to'lib-toshishining salbiy oqibatlarini qanday yengish mumkin?"
        ]
    },
    {
        "num": 2,
        "title": "An’anaviy, raqamli va ijtimoiy media turlari",
        "intro": "Aloqa texnologiyalari taraqqiyoti oqibatida ommaviy axborot vositalari bir necha davrlarni bosib o'tdi. An'anaviy bosma nashrlardan tortib sun'iy intellektga asoslangan zamonaviy ijtimoiy tarmoqlargacha bo'lgan media vositalari o'ziga xos tarqatish mexanizmiga ega.",
        "sections": [
            {
                "title": "1. An'anaviy media va ularning xususiyatlari",
                "paragraphs": [
                    "An'anaviy mediaga gazeta, jurnallar, radio va televideniye kiradi. Ularning asosiy xususiyati — bir tomonlama aloqadir (one-to-many). Ya'ni axborot tahririyat tomonidan tayyorlanadi va keng jamoatchilikka uzatiladi.",
                    "An'anaviy mediada tahririyat nazorati, korrektor va faktcheking bosqichlari kuchli bo'lgani sababli axborotning ishonchlilik darajasi odatda yuqoriroq bo'ladi, ammo tezkorlik raqamli mediaga qaraganda pastroqdir."
                ]
            },
            {
                "title": "2. Raqamli media va internet OAV",
                "paragraphs": [
                    "Raqamli media — axborotni raqamli formatda uzatuvchi axborot saytlari, internet portallari va podkast platformalaridir. Ular matn, video, audio va infografikani bir sahifada birlashtirish (multimediaylilik) imkoniyatini taqdim etadi.",
                    "Giperhavolalar tizimi o'quvchiga darhol birlamchi hujjat yoki tegishli qo'shimcha resursga o'tish imkonini beradi."
                ]
            },
            {
                "title": "3. Ijtimoiy tarmoqlar va algoritmlar ta'siri",
                "paragraphs": [
                    "Telegram, YouTube, Instagram kabi platformalarda har bir foydalanuvchi bir vaqtning o'zida ham iste'molchi, ham axborot tarqatuvchi (prosumer) hisoblanadi.",
                    "Ijtimoiy media algoritmlari foydalanuvchini platformada ko'proq ushlab turish uchun uning qiziqishlariga mos postlarni ko'rsatadi, bu esa «filtr pufagi» (filter bubble) fenomenini keltirib chiqaradi."
                ]
            }
        ],
        "diagram": {
            "title": "Media evolyutsiyasi va formatlari",
            "subtitle": "Axborot vositalarining tarqalish shakllari va o'ziga xosligi",
            "blocks": [
                {"title": "An'anaviy media", "text": "Gazeta, jurnal, TV, radio. Qat'iy tahririyat filtri, sekinroq, lekin yuqori mas'uliyat.", "insight": "Rasmiy va tahrirlangan", "badge": "AN'ANAVIY"},
                {"title": "Raqamli OAV", "text": "Veb-saytlar, onlayn xabarlar, podkastlar. Gipermatn, interaktivlik va giperhavolalar.", "insight": "Tezkor va multimediali", "badge": "RAQAMLI"},
                {"title": "Ijtimoiy tarmoqlar", "text": "Foydalanuvchi yaratadigan kontent. Yuqori tezlik, algoritmlar va filtr pufagi.", "insight": "Har kim muallif bo'la oladi", "badge": "IJTIMOIY"}
            ]
        },
        "conclusion": "Har bir media turining afzalligi va xatari bor: an'anaviy media chuqurlik va ishonchlilik bersa, raqamli media tezkorlik va qulaylik beradi.",
        "questions": [
            "An'anaviy medianing zamonaviy raqamli mediadan ustun va zaif tomonlari qaysilar?",
            "«Filtr pufagi» (Filter bubble) nima va u foydalanuvchi dunyoqarashiga qanday ta'sir qiladi?",
            "Raqamli mediada multimediaylilik nimalardan tashkil topadi?"
        ]
    },
    {
        "num": 3,
        "title": "Media namunalarini turi va vazifasiga ko‘ra tasniflash",
        "intro": "Auditoriyaga yetkazilayotgan har qanday media mahsulot aniq bir funksiyani bajaradi: u yangilik, tahlil, ma'rifiy saboq, tijoriy reklama yoki siyosiy targ'ibot bo'lishi mumkin. Ularni farqlash chalg'imaslik garovidir.",
        "sections": [
            {
                "title": "1. Media mahsulotlarining asosiy vazifalari",
                "paragraphs": [
                    "Axborot berish (informational): Jamiyatni ro'y berayotgan dolzarb hodisalar, qonunchilikdagi o'zgarishlar va hodisalardan xabardor qilish.",
                    "Ma'rifiy va tarbiyaviy (educational): Bilim va ko'nikmalarni oshirishga xizmat qiladigan ilmiy maqolalar, hujjatli filmlar, video darslar.",
                    "Ko'ngilochar (entertainment): Hordiq chiqarish, madaniy dam olishga qaratilgan musiqiy, kino va hazil-mutoyiba kontentlari."
                ]
            },
            {
                "title": "2. Tijoriy va yashirin reklama vositalari",
                "paragraphs": [
                    "Tijoriy kontent mahsulot yoki xizmatni sotish maqsadida yaratiladi. Ochiq reklama qonun talabiga binoan maxsus belgiga ega bo'lishi lozim.",
                    "Biroq ko'pincha «nativ reklama» yoki «yashirin targ'ibot» ko'rinishida foydali maqola yoki shaxsiy tavsiya niqobi ostida taqdim etiladi. Savodli o'quvchi bu farqni ilg'ay oladi."
                ]
            },
            {
                "title": "3. Tashviqot (propaganda) va mafkuraviy ta'sir",
                "paragraphs": [
                    "Tashviqot — ma'lum bir g'oya, siyosiy qarash yoki manfaatni keng omma ongiga singdirish uchun dalillarni biryoqlama tanlab ko'rsatish usulidir.",
                    "Unda hissiy chaqiriqlar, dushman qiyofasini yaratish va muqobil fikrlarni qoralash kabi manipulyativ usullardan keng foydalaniladi."
                ]
            }
        ],
        "diagram": {
            "title": "Media kontent vazifalari matrisi",
            "subtitle": "Xabarning asl maqsadi va uni auditoriyaga ta'sir kuchi",
            "blocks": [
                {"title": "Axborot va tahlil", "text": "Haqiqatni xolis yetkazish, o'quvchiga mustaqil xulosa chiqarish imkonini berish.", "insight": "Faktlar va raqamlar", "badge": "XOLIS"},
                {"title": "Reklama va tijorat", "text": "Xizmat yoki mahsulotni sotish, foyda ko'rish maqsadida qiziqish uyg'otish.", "insight": "Nativ targ'ibotni sezing", "badge": "TIJORIY"},
                {"title": "Tashviqot (Propaganda)", "text": "Bir tomonlama g'oyani majburan singdirish, qarama-qarshilik uyg'otish.", "insight": "Biryoqlama manfaatlarni fosh eting", "badge": "TASHVIQOT"}
            ]
        },
        "conclusion": "Xabarning vazifasini aniqlash unga qanday baho berish va qanchalik ishonish kerakligini belgilovchi bosh omildir.",
        "questions": [
            "Media mahsulotlarining 4 ta asosiy vazifasini sanab bering.",
            "Nativ reklama va xolis axborot maqolasi o'rtasidagi farqni qanday bilish mumkin?",
            "Propagandaning eng ko'p qo'llaniladigan 3 ta belgisini ayting."
        ]
    },
    {
        "num": 4,
        "title": "Axborot manbalari va qidiruv strategiyalari",
        "intro": "Internet cheksiz ma'lumotlar ummoni bo'lib, unda kerakli va aniq axborotni topish professional qidiruv madaniyatini talab qiladi. To'g'ri tanlangan qidiruv strategiyasi vaqtni tejaydi va noto'g'ri ma'lumotlarga duch kelish xavfini keskin kamaytiradi.",
        "sections": [
            {
                "title": "1. Axborot manbalarining turlari: birlamchi, ikkilamchi va uchlamchi",
                "paragraphs": [
                    "Birlamchi manbalar (Primary sources) — hodisaning bevosita ishtirokchisi tomonidan yaratilgan hujjatlar, rasmiy qonunlar, guvohlik ko'rsatmalari, ilmiy tajriba natijalari yoki arxiv ma'lumotlaridir.",
                    "Ikkilamchi manbalar — birlamchi ma'lumotlarni tahlil qilgan va umumlashtirgan ilmiy maqolalar, tahliliy yangiliklar va sharhlardir.",
                    "Uchlamchi manbalar — ensiklopediyalar, ma'lumotnomalar va umumiy bibliografik ro'yxatlardir."
                ]
            },
            {
                "title": "2. Mantiqiy operatorlar (Boolean search) yordamida qidiruv",
                "paragraphs": [
                    "Qidiruv tizimlarida (Google, Yandex) aniq natijalarga erishish uchun mantiqiy operatorlardan foydalaniladi:",
                    "«AND» (yoki bo'sh joy) — har ikkala so'zni o'z ichiga olgan sahifalarni topadi;",
                    "«OR» — kiritilgan so'zlardan kamida birini qidiradi;",
                    "Qo'shtirnoq («...») — so'z birikmasini matnda aynan shu tartibda qidirishni ta'minlaydi."
                ]
            },
            {
                "title": "3. Murakkab qidiruv modifikatorlari",
                "paragraphs": [
                    "«site:gov.uz» — qidiruvni faqat davlat organlari domenida amalga oshiradi;",
                    "«filetype:pdf» — faqat kitob, ilmiy hisobot yoki rasmiy qarorlar PDF faylini qidiradi;",
                    "«-istisno» (minus belgisi) — keraksiz yoki tijoriy natijalarni qidiruvdan chiqarib tashlaydi."
                ]
            }
        ],
        "diagram": {
            "title": "Samarali qidiruv strategiyasi tizimi",
            "subtitle": "Kalit so'zdan aniq ilmiy yoki rasmiy manbagacha",
            "blocks": [
                {"title": "Aniq kalit so'zlar", "text": "Umumiy iboralardan qochib, aniq ilmiy yoki rasmiy atamalarni tanlash.", "insight": "Sinonimlardan foydalaning", "step": True},
                {"title": "Filtr va operatorlar", "text": "site:, filetype:, «qo'shtirnoq» va minus modifikatorlarini qo'llash.", "insight": "Faqat ishonchli domenlarni qidiring", "step": True},
                {"title": "Birlamchi manba", "text": "Topilgan ma'lumotni rasmiy portal va asl hujjat bilan tasdiqlash.", "insight": "Ko'chirmakash saytlardan qoching", "step": True}
            ]
        },
        "conclusion": "Axborot izlash ko'nikmasi har bir tadqiqotchi va zamonaviy mutaxassisning vaqtini tejamkor sarflashining asosidir.",
        "questions": [
            "Birlamchi va ikkilamchi axborot manbalarining farqi nimada?",
            "Qidiruvda filetype:pdf va site:gov.uz operatorlari nima uchun kerak?",
            "Qo'shtirnoq ichiga olingan qidiruv so'zi qanday natija beradi?"
        ]
    },
    {
        "num": 5,
        "title": "Axborotni saralash va manbani baholash mezonlari",
        "intro": "Topilgan har qanday ma'lumotni haqiqat deb qabul qilish xatodir. Xalqaro mediata'limda ma'lumotlar sifatini baholash uchun maxsus CRAAP testi va manba ishonchliligi mezonlari ishlab chiqilgan.",
        "sections": [
            {
                "title": "1. CRAAP testi va uning tarkibiy qismlari",
                "paragraphs": [
                    "CRAAP testi axborotni 5 ta mezon bo'yicha tekshirishni nazarda tutadi:",
                    "Currency (Dolzarblik): Axborot qachon e'lon qilingan yoki yangilangan? Eski statistikadan foydalanilmayaptimi?",
                    "Relevance (Muvofiqlik): Ushbu ma'lumot sizning o'rganayotgan mavzungiz va maqsadli vazifangizga mos keladimi?",
                    "Authority (Muallif vakolati): Muallif kim? Uning sohada ilmiy darajasi, tajribasi yoki kasbiy obro'si bormi?",
                    "Accuracy (Aniqlik): Keltirilgan raqamlar va dalillar qayerdan olingan? Havolalar ishlayaptimi?",
                    "Purpose (Maqsad): Ushbu maqola xolis bilim berish uchun yozilganmi yoki nimadir sotish/targ'ib qilish uchunmi?"
                ]
            },
            {
                "title": "2. Veb-saytlar va domenlarning ishonchlilik darajasi",
                "paragraphs": [
                    "Sayt manzilining domen qismi manba haqida ko'p narsani aytib beradi. Masalan, .gov va .edu domenlari davlat va oliy ta'lim muassasalariga tegishli bo'lib, tekshirilgan kontentni taqdim etadi.",
                    "Notanish bepul xostingdagi (.xyz, .top, anonim bloglar) saytlarga shubha bilan qarash va ulardagi xabarlarni nufuzli nashrlar orqali qayta tekshirish shart."
                ]
            },
            {
                "title": "3. Mutaxassis xulosasi va peer-review ahamiyati",
                "paragraphs": [
                    "Ilmiy va kasbiy mavzularda eng ishonchli manba — bu mustaqil ekspertlar tomonidan ko'rib chiqilgan (peer-reviewed) ilmiy jurnallar va tahliliy hisobotlardir.",
                    "Ijtimoiy tarmoqdagi anonim blogerning fikri hech qachon rasmiy ilmiy xulosa yoki qonuniy dalil o'rnini bosa olmaydi."
                ]
            }
        ],
        "diagram": {
            "title": "CRAAP manba baholash tizimi",
            "subtitle": "Axborotning haqiqiyligini tekshirishning 5 ta oltin mezoni",
            "blocks": [
                {"title": "Dolzarblik (Currency)", "text": "Nashr etilgan va yangilangan sana. Ma'lumot eskirib qolmaganmi?", "insight": "Sanani doim tekshiring", "badge": "SANA"},
                {"title": "Muallif (Authority)", "text": "Yozuvchining mutaxassisligi, tashkiloti va ilmiy obro'si.", "insight": "Anonim xabarlarga ishonmang", "badge": "MUALLIF"},
                {"title": "Aniqlik (Accuracy)", "text": "Hujjatli manbalar, havolalar va dalillarning tekshirilganligi.", "insight": "Birlamchi dalilni toping", "badge": "DALIL"}
            ]
        },
        "conclusion": "Axborotni saralash — bu ongimizni zararli va yolg'on axborot axlatidan tozalovchi immunitetdir.",
        "questions": [
            "CRAAP testining 5 ta mezonini izohlab bering.",
            ".gov va .edu domenlarining boshqa ochiq domenlardan afzalligi nimada?",
            "Anonim muallif tomonidan e'lon qilingan statistikaga nima uchun ishonish mumkin emas?"
        ]
    }
]

# Add remaining 15 topics data sequentially
MORE_TOPICS = [
    {
        "num": 6,
        "title": "Kalit so‘z va filtrlar yordamida axborot izlash",
        "intro": "Amaliy qidiruv mashg'ulotlarida eng muhim ko'nikma — izlanayotgan mavzuni aniq kalit so'zlarga ajrata olish va qidiruv filtrlarini to'g'ri o'rnatishdir.",
        "sections": [
            {"title": "1. Kalit so'zlarni shakllantirish algoritmi", "paragraphs": ["Qidiruv so'rovini gap shaklida emas (masalan: 'qanday qilib media savodli bo'lish mumkin?'), balki tayanch tushunchalar kombinatsiyasi ko'rinishida tuzish lozim: 'media savodxonlik tamoyillari UNESCO'.", "Tegishli sinonimlar va xalqaro atamalarni qo'llash qidiruv maydonini kengaytiradi."]},
            {"title": "2. Vaqt va geografik cheklovlarni o'rnatish", "paragraphs": ["Qidiruv tizimlarining 'Tools' (Asboblar) menyusida vaqt oralig'ini (oxirgi hafta, oxirgi yil) belgilash eskirgan xabarlardan xalos bo'lishga yordam beradi.", "Hududiy cheklov esa aniq bir mamlakat qonunchiligi yoki statistikasi bo'yicha ma'lumot olishda juda qulay."]},
            {"title": "3. Multimodal qidiruv (rasm va ovoz orqali)", "paragraphs": ["Google Lens va Yandex Images orqali rasmni yuklab qidirish tasvirning asl nusxasini, uning internetga birinchi joylangan sanasini va haqiqiy kontekstini aniqlash imkonini beradi."]}
        ],
        "diagram": {
            "title": "Amaliy qidiruv filtrlash jarayoni",
            "subtitle": "Keng so'rovdan aniq maqsadli hujjatgacha",
            "blocks": [
                {"title": "Kalit so'z tanlash", "text": "Mavzuning tayanch so'zlarini ajratish va ortiqcha so'zlarni olib tashlash.", "insight": "Savol bermang, termin yozing", "step": True},
                {"title": "Vaqt va format filtri", "text": "Sanani oxirgi 1 yilga cheklash va fayl turini (masalan, PDF) tanlash.", "insight": "filetype:pdf dan foydalaning", "step": True},
                {"title": "Teskari qidiruv", "text": "Rasmlar va tasdiqlarni Google Lens orqali tekshirish.", "insight": "Tasvir asl sanasini toping", "step": True}
            ]
        },
        "conclusion": "To'g'ri sozlangan qidiruv filtrlari vaqtni 10 barobargacha tejaydi va feyk ma'lumotlarni elakdan o'tkazadi.",
        "questions": ["Qidiruvda vaqt filtrining ahamiyati nimada?", "Teskari tasvir qidiruvi (Reverse image search) qanday ishlaydi?", "So'rovni shakllantirishda nima uchun ortiqcha so'zlardan qochish kerak?"]
    },
    {
        "num": 7,
        "title": "Bir nechta manbani taqqoslash va vazifaga mosini tanlash",
        "intro": "Bitta manbaga asoslanib qilingan xulosa doimo subyektiv bo'lish xavfini tug'diradi. Shu sababli professional tahlilda doimo lateral o'qish (lateral reading) va bir nechta mustaqil manbalarni o'zaro solishtirish talab etiladi.",
        "sections": [
            {"title": "1. Lateral o'qish (Yonlama o'qish) usuli", "paragraphs": ["Klassik o'quvchi sahifani tepadan pastga qarab o'qiydi. Professional faktcheker esa maqolani ochgach, darhol brauzerda yangi tablarni ochib, ushbu manba va muallif haqida boshqa mustaqil saytlar nima deb yozganini o'rganadi.", "Bu usul ishonchsiz saytlarga vaqt sarflamaslikning eng samarali yo'lidir."]},
            {"title": "2. Manbalar o'rtasidagi ziddiyatlarni aniqlash", "paragraphs": ["Turli manbalarda bir xil hodisa bo'yicha turli raqamlar keltirilganda, birlamchi davlat idorasi yoki xalqaro tashkilotning rasmiy hisobotiga ustunlik beriladi.", "Ziddiyatli qarashlar mavjud bo'lganda, ularning barchasini xolis tahlil qilish lozim."]},
            {"title": "3. Vazifaga mos ishonchli manbani tanlash", "paragraphs": ["Ilmiy ish yoki rasmiy ma'ruza uchun faqat ilmiy nashrlar va normativ hujjatlar tanlanadi. Kundalik maishiy masalalar uchun esa sohaviy amaliy ko'rsatmalar kifoyadir."]}
        ],
        "diagram": {
            "title": "Lateral o'qish va manbalar sintezi",
            "subtitle": "Xolis haqiqatni aniqlashning ko'p tomonlama modeli",
            "blocks": [
                {"title": "1-Manba: Birlamchi hisobot", "text": "Vazirlik yoki rasmiy statistika organi raqamlari.", "insight": "Rasmiy ma'lumot", "badge": "RASMIY"},
                {"title": "2-Manba: Mustaqil OAV", "text": "Voqea joyidagi guvohlar va jurnalistlar surishtiruvi.", "insight": "Xolis tahlil", "badge": "MUSTAQIL"},
                {"title": "3-Manba: Soha eksperti", "text": "Fan arboblari va sohaviy mutaxassislarning xulosasi.", "insight": "Ilmiy asos", "badge": "EKSPERT"}
            ]
        },
        "conclusion": "Kamida 3 ta mustaqil manba bilan tasdiqlanmagan shov-shuvli xabarga hech qachon to'liq ishonmaslik kerak.",
        "questions": ["Lateral o'qish nima va uning afzalligi nimada?", "Manbalar o'rtasida ziddiyat bo'lsa qanday yo'l tutiladi?", "Triangulyatsiya (3 ta manba) qoidasi nima?"]
    },
    {
        "num": 8,
        "title": "Media xabarining muallifi, maqsadi va auditoriyasi",
        "intro": "Har qanday media xabarining ortida aniq bir inson yoki tashkilot turadi, u ma'lum bir auditoriyaga mo'ljallangan va qat'iy maqsadni ko'zlaydi. Ushbu uchlikni aniqlash xabarning asl mohiyatini fosh etadi.",
        "sections": [
            {"title": "1. Mualliflik va uning manfaatlarini aniqlash", "paragraphs": ["Muallif kim? U mustaqil jurnalistmi, siyosiy partiya vakilimi yoki biron tijoriy kompaniya xodimimi? Uning ushbu xabarni yozishdan moddiy yoki nufuz manfaati bormi?", "Manfaatlar to'qnashuvi mavjud bo'lgan xabarlarga alohida ehtiyotkorlik bilan munosabatda bo'lish zarur."]},
            {"title": "2. Maqsadli auditoriyani o'rganish", "paragraphs": ["Xabar kimga qaratilgan: yoshlar, onalar, nafaqaxo'rlar yoki tadbirkorlar? Har bir auditoriya uchun ta'sir qiluvchi psixologik 'richaglar' turlicha bo'ladi.", "Masalan, yoshlar auditoriyasi uchun yorqin dinamik vizual til, kattalar uchun esa xavfsizlik va barqarorlik ritorikasi tanlanadi."]},
            {"title": "3. Yashirin kun tartibi (Agenda setting)", "paragraphs": ["Media faqat nima haqida o'ylashimizni emas, balki qanday o'ylashimiz kerakligini ham shakllantirishga intilishi mumkin. Shuning uchun 'Muallif nima uchun aynan bugun bu xabarni tarqatmoqda?' degan savol berilishi shart."]}
        ],
        "diagram": {
            "title": "Media tahlilining kommunikativ uchligi",
            "subtitle": "Xabar mohiyatini ochuvchi uch asosiy savol",
            "blocks": [
                {"title": "Kim yaratdi? (Muallif)", "text": "Muallif shaxsi, vakolati, moliyalashtirish manbai va manfaati.", "insight": "Manfaatdorlikni tekshiring", "badge": "MUALLIF"},
                {"title": "Kim uchun? (Auditoriya)", "text": "Yosh, kasb, qiziqishlar va psixologik ta'sir nuqtalari.", "insight": "Hissiyotga ta'sirni o'rganing", "badge": "AUDITORIYA"},
                {"title": "Nima maqsadda? (Maqsad)", "text": "Xabardor qilish, ishontirish, sotish yoki kayfiyatni o'zgartirish.", "insight": "Yashirin maqsadni aniqlang", "badge": "MAQSAD"}
            ]
        },
        "conclusion": "Muallif, auditoriya va maqsad aniqlangandagina axborotning asl mohiyati va qimmati ochiladi.",
        "questions": ["Xabar muallifini bilish nima uchun muhim?", "Maqsadli auditoriya xabarning uslubiga qanday ta'sir qiladi?", "Agenda setting (kun tartibini shakllantirish) nima?"]
    },
    {
        "num": 9,
        "title": "Feyk xabar, dezinformatsiya va manipulyatsiya belgilari",
        "intro": "Zamonaviy axborot maydonida eng katta xavflardan biri bu atayin yolg'on tarqatish (dezinformatsiya) va odamlarni yanglishtirishdir. Yolg'on xabarlar ko'pincha haqiqatdan ko'ra 6 barobar tezroq tarqaladi.",
        "sections": [
            {"title": "1. Misinformatsiya, dezinformatsiya va malinformatsiya", "paragraphs": ["Misinformatsiya — adashib, bilmasdan tarqatilgan xato axborot (yomon niyat yo'q).", "Dezinformatsiya — bila turib, zarar yetkazish yoki odamlarni chalg'itish maqsadida yaratilgan soxta kontent.", "Malinformatsiya — haqiqiy shaxsiy ma'lumotlarni noqonuniy oshkor qilib, birovga qasddan zarar yetkazish."]},
            {"title": "2. Feyk xabarlarning asosiy alomatlari (Red flags)", "paragraphs": ["Qichqiruvchi va shov-shuvli sarlavhalar ('Daxshat!', 'Hamma bilsin!', 'Zudlik bilan ko'ring!');", "Matnda aniq sana, ism-sharif va rasmiy havola bo'lmasligi ('olimlarning aytishicha', 'bir tanishim aytdi');", "Grammatik xatolar, sifatsiz montaj qilingan suratlar va hissiy bosim o'tkazish ('tarqating, savob bo'ladi')."]},
            {"title": "3. Neyrotarmoqlar va Deepfake texnologiyalari", "paragraphs": ["Sun'iy intellekt yordamida yaratilgan soxta audio va video (deepfake) materiallar mashhur insonlar nomidan gapirtirilishi mumkin. Ularni yuz harakatidagi nomutanosiblik va lab sinxronizatsiyasi orqali aniqlash mumkin."]}
        ],
        "diagram": {
            "title": "Manipulyatsiya va feyk xabarlarni aniqlash",
            "subtitle": "Xavfli signallar va tekshiruv algoritmi",
            "blocks": [
                {"title": "Hissiy sarlavha", "text": "Vahima yoki haddan tashqari quvonch uyg'otuvchi chaqiriqlar.", "insight": "Hissiyotga berilmang", "badge": "SIGNAL 1"},
                {"title": "Manbaning yo'qligi", "text": "Hech qanday rasmiy tashkilot yoki birlamchi hujjatga havola yo'q.", "insight": "Rasmiy tasdiqni kuting", "badge": "SIGNAL 2"},
                {"title": "Soxta dalillar", "text": "Kontekstdan uzib olingan yoki fotomontaj qilingan tasvirlar.", "insight": "Faktcheking qiling", "badge": "SIGNAL 3"}
            ]
        },
        "conclusion": "Har qanday hissiy va shoshilinch xabarni ulashishdan oldin 'To'xta, o'yla, tekshir!' qoidasiga amal qilish shart.",
        "questions": ["Dezinformatsiya va misinformatsiya o'rtasidagi farq nima?", "Feyk xabarning eng ko'p uchraydigan 3 ta belgisini ayting?", "Deepfake qanday yaratiladi va uning xavfi nimada?"]
    },
    {
        "num": 10,
        "title": "Xabarni asl manba, sana va mustaqil dalillar orqali tekshirish",
        "intro": "Faktcheking — bu faktlarni tekshirishning professional texnologiyasidir. Ushbu darsda xabarning haqiqiyligini tekshirish bo'yicha amaliy harakatlar ketma-ketligi o'rganiladi.",
        "sections": [
            {"title": "1. Asl manbaga qaytish zanjiri (Trace back)", "paragraphs": ["Agar xabarda 'chet el nashrlariga ko'ra' deb yozilgan bo'lsa, birlamchi chet el saytini topib, unda aynan nima yozilganini o'qish shart. Ko'pincha tarjimada ma'no buzib ko'rsatiladi.", "Rasmiy qonun va hujjatlar faqat milliy qonunchilik bazalari (masalan, Lex.uz) orqali tasdiqlanadi."]},
            {"title": "2. Xronologik tekshiruv (Sana manipulyatsiyasi)", "paragraphs": ["Eng keng tarqalgan manipulyatsiya usuli — bu 5 yil oldingi tabiiy ofat yoki voqea videosini 'kecha ro'y berdi' deb qayta tarqatishdir.", "Arxiv tizimlari (Wayback Machine - archive.org) orqali veb-sahifaning o'tmishdagi ko'rinishini tekshirish mumkin."]},
            {"title": "3. Tasviriy va audiovizual tekshiruv", "paragraphs": ["Tasvirning metama'lumotlari (EXIF ma'lumotlari) rasmning qachon, qayerda va qaysi kamerada olinganini saqlaydi.", "Foto Forensics kabi vositalar tasvirda fotoshop yoki tahrirlash izlari bor-yo'qligini ko'rsatib beradi."]}
        ],
        "diagram": {
            "title": "Faktchekingning 3 bosqichli amaliy zanjiri",
            "subtitle": "Xabarning haqiqiyligini tekshirish algoritmi",
            "blocks": [
                {"title": "Birlamchi havola", "text": "Xabar boshlangan eng birinchi manbani va muallifni topish.", "insight": "Manba zanjirini kuzating", "step": True},
                {"title": "Sana va joylashuv", "text": "Voqea qachon va qayerda ro'y berganini xronologik solishtirish.", "insight": "Eski videolarga aldanmang", "step": True},
                {"title": "Ekspert tasdig'i", "text": "Factcheck.uz kabi xolis platformalar tahlilini ko'rib chiqish.", "insight": "Xolis tahlilchilar xulosasi", "step": True}
            ]
        },
        "conclusion": "Faktcheking madaniyati jamiyatda ishonchsiz axborot tarqalishining oldini oluvchi eng ishonchli to'siqdir.",
        "questions": ["Sana manipulyatsiyasi nima va u qanday fosh qilinadi?", "Wayback Machine sayti nima uchun xizmat qiladi?", "EXIF metama'lumotlari orqali qanday ma'lumotlarni bilib olish mumkin?"]
    },
    {
        "num": 11,
        "title": "Post, reklama yoki videoni tanqidiy tahlil qilish",
        "intro": "Vizual va matnli xabarlarni dekonstruksiya qilish (qismlarga ajratib tahlil qilish) orqali biz muallif qo'llagan yashirin psixologik ta'sirlarni ilg'ay olamiz.",
        "sections": [
            {"title": "1. Vizual kontent grammatikasi", "paragraphs": ["Kadr burchagi, yoritish va ranglar inson ongiga to'g'ridan-to'g'ri hissiy ta'sir ko'rsatadi. Pastdan olingan kadr qahramonni kuchli va ulug'vor qilib ko'rsatsa, yuqoridan olingan kadr uni zaif va himoyasiz ko'rsatadi.", "Musiqiy fon ham tomoshabinda sun'iy qo'rquv yoki tantana hissini uyg'otishi mumkin."]},
            {"title": "2. Reklamadagi yashirin va'dalar", "paragraphs": ["Tijoriy roliklarda ko'pincha mahsulotning o'zi emas, balki 'muvaffaqiyat', 'go'zallik' yoki 'baxt' tushunchasi sotiladi.", "Tahlilchi savol beradi: 'Ushbu mahsulot haqiqatan ham ushbu muammoni hal qiladimi yoki bu hissiy illyuziyami?'"]},
            {"title": "3. Argumentatsiya va mantiqiy xatolar", "paragraphs": ["Ko'pincha bahslarda 'odamga o'tish' (Ad Hominem) yoki 'soxta dilemma' (Yo biz bilansiz, yo bizga dushmansiz) kabi mantiqiy xatolar ishlatiladi.", "Mantiqiy xatolarni ajratish sog'lom fikrlashning asosidir."]}
        ],
        "diagram": {
            "title": "Media kontentni dekonstruksiya qilish",
            "subtitle": "Kadr, matn va tovush qatlamlarini ajratish",
            "blocks": [
                {"title": "Vizual qatlam", "text": "Kadr burchagi, rang gammasi, belgi va ramzlar tahlili.", "insight": "Ko'z aldanmasin", "badge": "TASVIR"},
                {"title": "Matn va nutq", "text": "Hissiy epitetlar, faktlar, mantiqiy xatolar va chaqiriqlar.", "insight": "Mantiqni tekshiring", "badge": "MATN"},
                {"title": "Ovoz va musiqa", "text": "Kayfiyat uyg'otuvchi orqa fon musiqasi va tovush effektlari.", "insight": "Hissiy manipulyatsiyani sezing", "badge": "TOVUSH"}
            ]
        },
        "conclusion": "Dekonstruksiya ko'nikmasi foydalanuvchini vizual afsun va manipulyatsiyalardan to'liq himoya qiladi.",
        "questions": ["Kadr burchagi (rakurs) tomoshabin idrokiga qanday ta'sir qiladi?", "Ad Hominem mantiqiy xatosi nima?", "Reklama roligida yashirin va'dani qanday aniqlash mumkin?"]
    },
    {
        "num": 12,
        "title": "Media mahsulotini rejalash va ssenariy tuzish",
        "intro": "Har qanday sifatli media mahsulot (maqola, videorolik, podkast) puxta ishlab chiqilgan reja va ssenariydan boshlanadi. Tizimli rejalashtirish g'oyani aniq va jozibador shaklda yetkazish garovidir.",
        "sections": [
            {"title": "1. G'oyadan mavzugacha: Maqsadni shakllantirish", "paragraphs": ["Ijodiy jarayon 'Men ushbu kontent orqali auditoriyaga nima aytmoqchiman?' degan aniq bosh g'oyani (Logline) shakllantirishdan boshlanadi.", "Mavzu auditoriya ehtiyojlariga mos kelishi va dolzarb muammoni ko'tarishi lozim."]},
            {"title": "2. Klassik dramaturgiya va ssenariy strukturasi", "paragraphs": ["Uch qismli kompozitsiya: Kirish (Ekspozitsiya va muammo), Rivojlanish (Voqealar zanjiri va kulminatsiya) hamda Yechim (Xulosa va harakatga chaqiriq - Call to Action).", "Ushbu tizim tomoshabin e'tiborini boshidan oxirigacha ushlab turishni ta'minlaydi."]},
            {"title": "3. Raskadrovka (Storyboard) tayyorlash", "paragraphs": ["Raskadrovka — bo'lajak videoning har bir kadri, matni va ovozini qog'ozda yoki raqamli vositada ketma-ket chizib chiqishdir.", "Bu jarayon suratga olishda ortiqcha xarajat va vaqt yo'qotilishining oldini oladi."]}
        ],
        "diagram": {
            "title": "Media ishlab chiqarish quvuri (Pipeline)",
            "subtitle": "G'oyadan tayyor mahsulotgacha bo'lgan bosqichlar",
            "blocks": [
                {"title": "Pre-production", "text": "Mavzu tanlash, fakt yig'ish, ssenariy yozish va raskadrovka.", "insight": "Reja barcha narsaning asosi", "step": True},
                {"title": "Production", "text": "Suratga olish, audio yozish, intervyu va ko'rgazmalar tayyorlash.", "insight": "Sifatli yorug'lik va ovoz", "step": True},
                {"title": "Post-production", "text": "Video montaj, rang korreksiyasi, titrlar va tarqatish.", "insight": "Auditoriyaga yetkazish", "step": True}
            ]
        },
        "conclusion": "Yaxshi yozilgan ssenariy — bu mahsulotning 70% muvaffaqiyatidir.",
        "questions": ["Media mahsulotida 'Logline' nima?", "Uch qismli ssenariy strukturasining tarkibiy qismlarini ayting.", "Raskadrovka (Storyboard) nima uchun kerak?"]
    },
    {
        "num": 13,
        "title": "Matn, tasvir va videoni auditoriyaga mos taqdim etish",
        "intro": "Bir xil axborotni turli platformalar va auditoriyalar uchun turlicha taqdim etish zarur. Taqdimot formati auditoriyaning yoshi, ehtiyoji va foydalanayotgan qurilmasiga mos kelishi shart.",
        "sections": [
            {"title": "1. Axborotni vizuallashtirish qoidalari", "paragraphs": ["Inson miyasi vizual tasvirlarni matnga qaraganda 60 000 marta tezroq idrok qiladi. Murakkab raqamlar va jarayonlarni grafik, diagramma va sxemalar orqali ko'rsatish tushunishni osonlashtiradi.", "Biroq vizual elementlar haddan tashqari ko'p bo'lsa, o'quvchini asosiy fikrdan chalg'itishi mumkin."]},
            {"title": "2. Platformalar xususiyatiga moslashish (Cross-platform)", "paragraphs": ["Rasmiy sayt yoki tahliliy maqola uchun keng hajmli, faktlarga boy uzun matn (longread) mos keladi.", "Telegram yoki mobil ilovalar uchun esa qisqa, tez o'qiladigan tezislar va infografikalar tanlanadi. YouTube uchun gorizontal video, Reels va Shorts uchun vertikal dinamik format talab etiladi."]},
            {"title": "3. Tipografika va o'qiluvchanlik (Readability)", "paragraphs": ["Shriftlar soni 2 tadan oshmasligi, matn va fon o'rtasida yetarli kontrast bo'lishi hamda qatorlar orasidagi masofa qulay bo'lishi auditoriyani toliqtirmaydi."]}
        ],
        "diagram": {
            "title": "Platforma va format moslashuvi",
            "subtitle": "Bir g'oyani turli auditoriyalarga moslash modeli",
            "blocks": [
                {"title": "Sayt / Maqola", "text": "Batafsil matn, tahliliy jadvallar, hujjatli havolalar va ekspert fikrlari.", "insight": "Chuqur o'rganuvchilar uchun", "badge": "LONGREAD"},
                {"title": "Ijtimoiy tarmoq", "text": "Qisqa postlar, infografika, asosiy tezislar va interaktiv savollar.", "insight": "Tezkor o'quvchilar uchun", "badge": "MOBIL"},
                {"title": "Vertikal video", "text": "15-60 soniyali dinamik rolik, titrlar bilan boyitilgan kontent.", "insight": "Yoshlar auditoriyasi uchun", "badge": "SHORTS/REELS"}
            ]
        },
        "conclusion": "Auditoriyaning axborot iste'mol qilish odatini bilmasdan turib samarali media kontent yaratib bo'lmaydi.",
        "questions": ["Nima uchun vizual ma'lumot matnga qaraganda tezroq idrok qilinadi?", "Gorizontal va vertikal video formatlarining farqi va qo'llanish o'rni qayerda?", "Tipografikada kontrastning ahamiyati nimada?"]
    },
    {
        "num": 14,
        "title": "Matnli va tasvirli kontent yoki infografika yaratish",
        "intro": "Infografika — bu murakkab axborot va statistikani tushunarli, chiroyli va ixcham shaklda ifodalash san'atidir. Amaliy mashg'ulotda zamonaviy dizayn tamoyillari va infografika yaratish bosqichlari o'rganiladi.",
        "sections": [
            {"title": "1. Infografikaning turlari", "paragraphs": ["Statistik infografika: Diagrammalar va foizli ko'rsatkichlar orqali raqamlarni qiyoslash.", "Jarayonli infografika: Bosqichma-bosqich harakatlar algoritmi (flowchart).", "Qiyosiy infografika: Ikki yoki undan ortiq hodisani o'zaro solishtirish (Afzalliklar va kamchiliklar)."]},
            {"title": "2. Ranglar psixologiyasi va ierarxiya", "paragraphs": ["Ranglar axborotning muhimlik darajasini belgilaydi. Eng muhim raqam yoki xulosa kontrastli yorqin rang bilan ajratiladi.", "Vizual ierarxiya ko'zning tabiiy harakatiga (Z-shakli yoki F-shakli) mos ravishda tuzilishi lozim."]},
            {"title": "3. Zamonaviy onlayn vositalar", "paragraphs": ["Canva, Piktochart, Figma kabi platformalar tayyor shablonlar yordamida tez va professional infografikalar tayyorlash imkonini beradi.", "Biroq har qanday dizayndan oldin dastlab ma'lumotlarni saralab, xatolarini tekshirib olish shart."]}
        ],
        "diagram": {
            "title": "Sifatli infografika tayyorlash ketma-ketligi",
            "subtitle": "Xom raqamlardan tushunarli vizual plakatgacha",
            "blocks": [
                {"title": "Ma'lumotni tozalash", "text": "Ortiqcha raqamlarni olib tashlab, faqat eng muhim 3-4 ko'rsatkichni ajratish.", "insight": "Ko'p ma'lumot bilan to'ldirmang", "step": True},
                {"title": "Vizual struktura", "text": "Mantiqiy bloklar va vizual ierarxiya (F-pattern) bo'yicha joylashtirish.", "insight": "Ko'z yo'nalishini hisobga oling", "step": True},
                {"title": "Manbani ko'rsatish", "text": "Infografika pastida ma'lumot olingan rasmiy manba va muallifni ko'rsatish.", "insight": "Manbasiz grafika ishonchsizdir", "step": True}
            ]
        },
        "conclusion": "Mukammal infografika — bu matnsiz ham asosiy g'oyani anglatib bera oladigan vositadir.",
        "questions": ["Infografikaning qanday asosiy turlari mavjud?", "Vizual ierarxiya nima va u o'quvchiga qanday yordam beradi?", "Infografikada manba ko'rsatish nega majburiydir?"]
    },
    {
        "num": 15,
        "title": "Qisqa videoxabar tayyorlash va taqdim etish",
        "intro": "Bugungi kunda video eng ta'sirchan media formati hisoblanadi. Qisqa, lo'nda va mazmunli videoxabarlar ijtimoiy tarmoqlarda millionlab auditoriyaga yetib borishi mumkin.",
        "sections": [
            {"title": "1. Dastlabki 3 soniya qoidasi (Hook)", "paragraphs": ["Mobil formatda tomoshabin videoning qolgan qismini ko'rish-ko'rmasligini dastlabki 3 soniyada hal qiladi.", "Shu sababli videoni uzoq salomlashish bilan emas, balki qiziqarli savol, kutilmagan fakt yoki vizual dinamika bilan boshlash lozim."]},
            {"title": "2. Ovoz sifati va yoritish", "paragraphs": ["Tomoshabin sifatsiz tasvirni kechirishi mumkin, lekin tushunarsiz va shovqinli ovoz bo'lsa videoni darhol o'chiradi. Petlichka mikrofonidan foydalanish ovoz tozaligini ta'minlaydi.", "Yorug'lik manbai kameraning orqasida emas, balki qahramonning yuziga qaratilgan bo'lishi kerak."]},
            {"title": "3. Subtitrlar va dinamik montaj", "paragraphs": ["Foydalanuvchilarning 70% dan ortig'i ijtimoiy tarmoqdagi videolarni tovushsiz tomosha qiladi. Shu bois ekranga aniq subtitrlar (titrlar) joylashtirish qat'iy talabdir.", "Kadrlar har 4-6 soniyada yangilanib turishi tomoshabin diqqatini ushlab turadi."]}
        ],
        "diagram": {
            "title": "Qisqa video anatomiyasi (Reels/Shorts)",
            "subtitle": "Tomoshabin e'tiborini ushlab turuvchi bloklar",
            "blocks": [
                {"title": "0-3 soniya: Ilgak (Hook)", "text": "Diqqatni tortuvchi savol, muammo yoki shov-shuvsiz fakt.", "insight": "Sekin boshlamang", "badge": "ILGAK"},
                {"title": "3-45 soniya: Asosiy mazmun", "text": "Muammoning yechimi, amaliy maslahat yoki tahliliy ko'rsatma.", "insight": "Ortiqcha so'zlarsiz", "badge": "YECHIM"},
                {"title": "45-60 soniya: Xulosa / CTA", "text": "Fikr bildirishga undash, manbaga havola yoki yakuniy xulosa.", "insight": "Savol bilan yakunlang", "badge": "XULOSA"}
            ]
        },
        "conclusion": "Samarali video — bu tomoshabinning vaqtini hurmat qilgan holda eng kerakli ma'lumotni lo'nda yetkazishdir.",
        "questions": ["Videoda dastlabki 3 soniyaning hal qiluvchi roli nimada?", "Ovoz sifati nima uchun tasvir sifatidan muhimroq hisoblanadi?", "Videoga subtitr qo'yish nima sababdan majburiydir?"]
    },
    {
        "num": 16,
        "title": "Media etikasi va netiket",
        "intro": "Axborot erkinligi chegarasiz emas, u boshqalarning sha'ni, qadr-qimmati va huquqlari bilan chegaralanadi. Tarmoqda o'zini tutish odobi (netiket) va media etikasi raqamli madaniyatning poydevoridir.",
        "sections": [
            {"title": "1. Netiket — tarmoq odob-axloq qoidalari", "paragraphs": ["Katta harflar (CAPS LOCK) bilan yozish baqirish va agressiya deb qabul qilinadi.", "Birovning ruxsatisiz uning shaxsiy fotosuratlari yoki xatlarini e'lon qilish odobsizlik va qonunbuzarlikdir.", "Guruh va chatlarda faqat mavzuga doir (offtopiksiz) yozish boshqalarning vaqtini hurmat qilish belgisidir."]},
            {"title": "2. Xolislik, halollik va manfaatlar to'qnashuvi", "paragraphs": ["Axborot tarqatuvchi shaxs o'z manfaatlarini ommaga oshkor qilishi lozim. Agar biron loyihadan pul olgan bo'lsa, buni ochiq tan olishi halollik mezonidir.", "Boshqalar sha'niga asossiz tuhmat qilish yoki kamsituvchi laqablar qo'yish qat'iyan man etiladi."]},
            {"title": "3. Xatoni tan olish va radziya (retraction) berish madaniyati", "paragraphs": ["Agar noto'g'ri ma'lumot tarqatilgani aniqlansa, uni shunchaki o'chirib yuborish yetarli emas, balki rasmiy raddiya berib, auditoriyadan uzr so'rash kasbiy etikaning oliy namunasidir."]}
        ],
        "diagram": {
            "title": "Raqamli muloqot va netiket tamoyillari",
            "subtitle": "Tarmoqda madaniyatli va mas'uliyatli muloqot qoidalari",
            "blocks": [
                {"title": "Shaxsni hurmat qilish", "text": "Haqorat, tahdid va kamsitishlardan tiyilish, boshqalar fikrini tinglash.", "insight": "Har bir profil ortida inson bor", "badge": "HURMAT"},
                {"title": "Maxfiylikni saqlash", "text": "Birovning xabarlari, suratlari va shaxsiy ma'lumotlarini ruxsatsiz ulashmaslik.", "insight": "Daxlsizlik huquqini buzmang", "badge": "MAXFIYLIK"},
                {"title": "Xolis va asosli fikr", "text": "Trollik va guruhli tajovuzda ishtirok etmaslik, faktlarga tayanish.", "insight": "Kiberbobo bo'lmang", "badge": "ODOB"}
            ]
        },
        "conclusion": "Virtual olamda yozgan har bir sharhingiz va xabaringiz sizning haqiqiy hayotdagi madaniyatingiz ko'zgusidir.",
        "questions": ["Netiket tushunchasi nimani anglatadi?", "Tarmoqda katta harflar bilan yozish nima uchun odobsizlik sanaladi?", "Raddiya (retraction) nima va u qachon beriladi?"]
    },
    {
        "num": 17,
        "title": "Mualliflik huquqi, plagiat va manba ko‘rsatish",
        "intro": "Intellektual mulk — bu inson aqliy mehnati natijasidir. Birovning g'oyasi, matni, surati yoki musiqasidan noqonuniy foydalanish intellektual o'g'irlik (plagiat) sanaladi va qonuniy javobgarlikka sabab bo'ladi.",
        "sections": [
            {"title": "1. Mualliflik huquqi (Copyright) asoslari", "paragraphs": ["Asar yaratilgan paytdan boshlab mualliflik huquqi yuzaga keladi (maxsus ro'yxatdan o'tkazish shart emas).", "Muallifning ruxsatisiz uning asaridan tijoriy maqsadda foydalanish qat'iyan taqiqlanadi."]},
            {"title": "2. Plagiat va uning turlari", "paragraphs": ["To'g'ridan-to'g'ri plagiat: Birovning matnini so'zma-so'z ko'chirib, o'z nomidan e'lon qilish.", "Parafrazli plagiat: Birovning g'oyasini o'z so'zlari bilan aytib, lekin asl manbaga havola bermaslik.", "O'z-o'zini plagiat qilish: O'zining eski ishini yangi ilmiy ish sifatida qayta topshirish."]},
            {"title": "3. Creative Commons litsenziyalari va Fair Use", "paragraphs": ["Creative Commons (CC) — mualliflarga o'z asarlarini erkin tarqatish shartlarini belgilash imkonini beruvchi litsenziyalar tizimidir.", "CC-BY litsenziyasi muallif nomini ko'rsatgan holda asardan erkin foydalanishga ruxsat beradi. Ta'lim maqsadida qisqa iqtibos olishga (Fair Use) yo'l qo'yiladi."]}
        ],
        "diagram": {
            "title": "Litsenziyalar va qonuniy foydalanish xaritasi",
            "subtitle": "Mualliflik huquqini buzmasdan materiallardan foydalanish",
            "blocks": [
                {"title": "Public Domain (Ochiq mulk)", "text": "Muddati tugagan yoki muallif erkin foydalanishga topshirgan asarlar.", "insight": "Cheklovlarsiz foydalanish", "badge": "OCHIQ"},
                {"title": "Creative Commons (CC)", "text": "Muallif nomini ko'rsatish sharti bilan bepul foydalanish mumkin.", "insight": "CC-BY qoidalariga rioya qiling", "badge": "LITSENZIYA"},
                {"title": "Barcha huquqlar himoyalangan", "text": "Muallifning yozma ruxsatisiz nusxa ko'chirish qat'iyan taqiqlanadi.", "insight": "Ruxsatsiz foydalanmang", "badge": "HIMOYA"}
            ]
        },
        "conclusion": "Birovning mehnatini qadrlash va unga havola berish har bir ziyoli insonning axloqiy burchidir.",
        "questions": ["Plagiat nima va uning oqibatlari qanday?", "Creative Commons litsenziyalarining mohiyati nimada?", "Fair Use (Halol foydalanish) qoidasi ta'limda qanday qo'llanadi?"]
    },
    {
        "num": 18,
        "title": "Shaxsiy ma’lumotlar, raqamli gigiyena va kiberbulling",
        "intro": "Raqamli xavfsizlik — bu virtual olamda shaxsiy daxlsizlik, moliyaviy ma'lumotlar va ruhiy xotirjamlikni himoya qilish choralari majmuidir.",
        "sections": [
            {"title": "1. Shaxsiy ma'lumotlar va 'Raqamli iz' (Digital footprint)", "paragraphs": ["Biz internetda qoldiradigan har bir iz: qidiruvlar, layklar, joylashuv (geolokatsiya) va xaridlar yagona raqamli portretimizni yaratadi.", "Pasport nusxasi, bank kartasi ma'lumotlari, yashash manzili va shaxsiy telefon raqamini ochiq guruhlarda e'lon qilish kiberfiribgarlarga yo'l ochadi."]},
            {"title": "2. Raqamli gigiyena qoidalari", "paragraphs": ["Har bir hisob uchun alohida murakkab parol (kamida 12 ta belgi: harflar, raqamlar, belgilar) o'rnatish;", "Ikki bosqichli autentifikatsiya (2FA) funksiyasini barcha ijtimoiy tarmoq va pochtalarda yoqish;", "Jamoat joylaridagi ochiq Wi-Fi tarmoqlarida bank operatsiyalarini bajarmaslik."]},
            {"title": "3. Kiberbulling va undan himoyalanish", "paragraphs": ["Kiberbulling — tarmoqda insonni tizimli kamsitish, haqoratlash, qo'rqitish yoki unga qarshi jamoaviy nafrat uyg'otishdir.", "Bunday holatda tajovuzkor bilan bahslashmaslik, darhol skrinshotlarni saqlab olish, foydalanuvchini bloklash va huquqni muhofaza qiluvchi organlarga murojaat qilish lozim."]}
        ],
        "diagram": {
            "title": "Raqamli xavfsizlik qalqoni",
            "subtitle": "Kiberxavflardan himoyalanishning 3 asosiy ustuni",
            "blocks": [
                {"title": "2FA va Parollar", "text": "Kuchli parollar va ikki bosqichli SMS/Ilova tasdig'ini yoqish.", "insight": "Bitta parolni hamma joyda ishlatmang", "badge": "XAVFSIZLIK"},
                {"title": "Maxfiylik sozlamalari", "text": "Profilingizni begonalardan yopish, geolokatsiyani o'chirib qo'yish.", "insight": "Shaxsiy ma'lumotni himoyalang", "badge": "MAXFIYLIK"},
                {"title": "Agressiyaga to'siq", "text": "Kiberbulling va shubhali xabarlarga javob bermaslik, 'Blok' tugmasi.", "insight": "Tajovuzkorga javob bermang", "badge": "HIMOYA"}
            ]
        },
        "conclusion": "Raqamli gigiyena — bu xuddi qo'lni sovunlab yuvish kabi har kungi shaxsiy xavfsizlik odatiga aylanishi shart.",
        "questions": ["Raqamli iz nima va u qanday xavf tug'dirishi mumkin?", "Ikki bosqichli autentifikatsiya (2FA) qanday ishlaydi?", "Kiberbullingga uchragan shaxs qanday harakat qilishi kerak?"]
    },
    {
        "num": 19,
        "title": "Media mahsulotiga iqtibos va manba havolalarini to‘g‘ri kiritish",
        "intro": "Ilmiy, tahliliy yoki ta'limiy media mahsulotlar yaratishda foydalanilgan adabiyotlar va birlamchi manbalarga to'g'ri havola berish akademik halollik talabidir.",
        "sections": [
            {"title": "1. Iqtibos keltirish usullari: To'g'ridan-to'g'ri va bilvosita", "paragraphs": ["To'g'ridan-to'g'ri iqtibos: Muallif fikri hech qanday o'zgarishsiz, qo'shtirnoq ichida keltiriladi va qavs ichida muallif hamda sahifa ko'rsatiladi.", "Bilvosita iqtibos (parafraz): Muallif fikri o'z so'zlari bilan bayon etiladi, lekin g'oya egasiga manba havolasi qo'yiladi."]},
            {"title": "2. Xalqaro iqtibos standartlari (APA, MLA, GOST)", "paragraphs": ["APA standarti: Muallif familiyasi, nashr yili va sahifani talab qiladi (Masalan: Alimov, 2024, 45-bet).", "Raqamli mediada esa giperhavolalar (hyperlinks) orqali to'g'ridan-to'g'ri asl manba veb-sahifasiga yo'naltirish qulay usuldir."]},
            {"title": "3. Bibliografik ro'yxatni shakllantirish", "paragraphs": ["Asar so'ngida foydalanilgan barcha kitoblar, qonun hujjatlari va internet saytlari alifbo tartibida, to'liq nashriyot ma'lumotlari bilan ko'rsatilishi shart."]}
        ],
        "diagram": {
            "title": "Iqtibos va havolalarni rasmiylashtirish tizimi",
            "subtitle": "Akademik va tahliliy halollik mezonlari",
            "blocks": [
                {"title": "To'g'ridan-to'g'ri iqtibos", "text": "Qo'shtirnoq ichida aynan ko'chirish + muallif va sahifa.", "insight": "So'zlarni o'zgartirmang", "badge": "IQTIBOS"},
                {"title": "Parafraz (Mazmun)", "text": "Fikrni o'z so'zingiz bilan aytish + g'oya egasiga havola.", "insight": "Plagiatga yo'l qo'ymang", "badge": "PARAFRAZ"},
                {"title": "Raqamli giperhavola", "text": "Internet manbasi URL manziliga to'g'ri gipermatn bog'lash.", "insight": "Ishlaydigan havolalar bering", "badge": "HAVOLA"}
            ]
        },
        "conclusion": "To'g'ri qo'yilgan iqtiboslar muallifning fikrlarini yanada salmoqli, ishonchli va nufuzli qiladi.",
        "questions": ["To'g'ridan-to'g'ri va bilvosita iqtibosning farqi nimada?", "Raqamli matnlarda giperhavolaning afzalligi nimada?", "Bibliografik ro'yxat qanday tartibda tuziladi?"]
    },
    {
        "num": 20,
        "title": "Maxfiylik va onlayn muloqotga oid vaziyatlarni yechish",
        "intro": "Mazkur yakuniy mavzuda kurs davomida o'rganilgan nazariy bilimlar real hayotiy keyslar (Case study) va muammoli vaziyatlar misolida amaliy mustahkamlanadi.",
        "sections": [
            {"title": "1. 1-Keys: Shov-shuvli feyk xabarni fosh etish", "paragraphs": ["Vaziyat: Telegram guruhlarida 'Ertadan boshlab barcha bank kartalaridan foiz ushlab qolinadi' degan ovozli xabar tarqaldi.", "Yechish algoritmi: Markaziy bank rasmiy saytini tekshirish, ovozli xabarning anonimligini ko'rish, rasmiy raddiyani topish va guruhdagilarni ogohlantirish."]},
            {"title": "2. 2-Keys: Ruxsatsiz shaxsiy fotosuratlarning tarqalishi", "paragraphs": ["Vaziyat: Talabaning shaxsiy surati uning ruxsatisiz tanishlari tomonidan ijtimoiy tarmoqqa kulgili mem sifatida joylandi.", "Yechish algoritmi: Platforma ma'muriyatiga shikoyat (Report) yuborish, O'zbekiston Respublikasining 'Shaxsga doir ma'lumotlar to'g'risida'gi Qonuniga muvofiq o'chirishni talab qilish."]},
            {"title": "3. 3-Keys: Mualliflik huquqini buzmasdan taqdimot tayyorlash", "paragraphs": ["Vaziyat: Yangi loyiha taqdimoti uchun internetdan rasmlar va musiqa tanlash kerak.", "Yechish algoritmi: Google rasmlar qidiruvida 'Creative Commons litsenziyasi' filtrini yoqish, bepul stok saytlari (Unsplash, Pexels)dan foydalanish va har bir slayd tagida muallifni ko'rsatish."]}
        ],
        "diagram": {
            "title": "Muammoli vaziyatlarni hal qilish algoritmi",
            "subtitle": "Kritik vaziyatlarda to'g'ri qadamlar ketma-ketligi",
            "blocks": [
                {"title": "1-Qadam: Tahlil", "text": "Vaziyatni hissiyotsiz baholash, xavf va huquqbuzarlik turini aniqlash.", "insight": "Shoshilmang", "step": True},
                {"title": "2-Qadam: Huquqiy asos", "text": "Qonun normalari, platforma qoidalari va faktcheking vositalari.", "insight": "Qonunga tayaning", "step": True},
                {"title": "3-Qadam: Harakat", "text": "Raddiya berish, hisobni himoyalash yoki rasmiy murojaat qilish.", "insight": "Muammoni tizimli hal qiling", "step": True}
            ]
        },
        "conclusion": "Media va axborot savodxonligi — bu zamonaviy jamiyatda har bir fuqaroning erkin, ongli va xavfsiz yashashini kafolatlovchi raqamli madaniyatdir.",
        "questions": ["Anonim audio xabar kelganda birinchi navbatda nima qilish kerak?", "Shaxsiy surat ruxsatsiz tarqalganda qaysi qonuniy choralar ko'riladi?", "Taqdimotda foydalanish uchun bepul va xavfsiz rasmlarni qayerdan topish mumkin?"]
    }
]

ALL_TOPICS = TOPICS_DATA + MORE_TOPICS
print(f"Total topics loaded: {len(ALL_TOPICS)}")

namespace MediaSavodxonlik;

public enum VisualLessonType
{
    ThreeStepProcess,     // Bosqichma-bosqich jarayon (1 -> 2 -> 3)
    ComparisonMatrix,     // Qiyosiy tahlil / Solishtirish jadvali
    AnatomyDeconstruction,// Post / Xabar anatomiyasi va elementlari
    DecisionTree,         // Qaror qabul qilish algoritmi / flowchart
    RedFlagsInspector,    // Xavf / Manipulyatsiya signallari va qizil bayroqlar
    EvidenceVerification, // Dalillar zanjiri va faktcheking
    StoryboardPipeline,   // Video / Ssenariy kadrlar ketma-ketligi
    AudienceFormatMatrix, // Auditoriya va format mosligi matrisi
    InfographicChecklist, // Infografika anatomiyasi va mezonlari
    NetiquetteDialogue,   // Dialog va muloqot simulyatsiyasi (Noto'g'ri vs To'g'ri)
    CopyrightLicenseGuide,// Litsenziyalar va mualliflik huquqi xaritasi
    DigitalHygieneShield  // Xavfsizlik qalqoni va shaxsiy ma'lumotlar
}

internal static class VisualLessons
{
    internal sealed record VisualStep(string StepNumber, string Title, string Description, string Tip, string Icon);
    
    internal sealed record InteractiveElement(string Title, string Status, string Detail, string Recommendation, string TagColor);

    internal sealed record Board(
        string Title,
        string Subtitle,
        string Nodes, 
        string Notes, 
        string Scenario, 
        string Takeaway,
        VisualLessonType LessonType,
        string DiagramBadge,
        string CoreConcept,
        InteractiveElement[] InteractiveItems
    )
    {
        public string[] Labels => Nodes.Split('|');
        public string[] Details => Notes.Split('|');
    }

    private static readonly Board[] Boards =
    [
        // 1-MAVZU: Media va axborot savodxonligi asoslari
        new(
            Title: "Xabarni tanqidiy tahlil qilishning 3 bosqichli modeli",
            Subtitle: "Axborot oqimida xolislikni saqlash va faktni fikrdan ajratish",
            Nodes: "01. Kuzating va qayd eting|02. Talqin va maqsadni ajrating|03. Mustaqil dalil bilan tekshiring",
            Notes: "Xabardagi hissiy iboralarni chetga surib, aynan nima voqea yoki fakt bayon qilinganini belgilang.|Muallif voqeani qanday bo'yoqlarda tasvirlamoqda? Muallifning shaxsiy fikri fakt o'rnida berilmaganmi?|Birlamchi rasmiy manba, statistika yoki hujjat mavjudligini xolis tekshiring.",
            Scenario: "Guruh chatida «Internet birdan sekinlashdi, sababi yangi o‘rnatilgan router nosoz va xavfli ekan!» degan vahimali audio tarqaldi.",
            Takeaway: "Internetning haqiqiy tezligi maxsus testlar (Speedtest) bilan o'lchanadi. Routerning nosozligi haqidagi gap esa dalilsiz taxmindir. Xulosa qilishdan oldin texnik tekshiruv shart.",
            LessonType: VisualLessonType.ThreeStepProcess,
            DiagramBadge: "TAHLIL ALGORITMI",
            CoreConcept: "Fakt — o'lchanadigan va isbotlanadigan haqiqat; Talqin — subyektiv qarash.",
            InteractiveItems:
            [
                new("Audio xabar formati", "Hissiy bosim", "«Hamma bilsin», «Zudlik bilan tarqating» kabi so'zlar kishida shoshqaloqlik uyg'otadi.", "Vahimaga berilmasdan xabarni tahlil qilish", "#FDA4AF"),
                new("Tezlik pasayishi", "Haqiqiy alomat", "Tarmoq sekinlashuvi provayder profilaktikasi yoki yuklama tufayli ham bo'lishi mumkin.", "Boshqa qurilmalarda ham ulanishni tekshirish", "#93C5FD"),
                new("Router aybi da'vosi", "Asossiz xulosa", "Hech qanday diagnostika dalili keltirilmagan, texnik asos yo'q.", "Mutaxassis yoki ma'muriyat xulosasini kutish", "#FDBA74")
            ]
        ),

        // 2-MAVZU: An’anaviy, raqamli va ijtimoiy media turlari
        new(
            Title: "Media kanallarining qiyosiy xaritasi va ta'sir doirasi",
            Subtitle: "Bosma, elektron (veb/radio/TV) va ijtimoiy tarmoqlar o'rtasidagi tafovut",
            Nodes: "Bosma media (Stend/Gazeta)|Elektron rasmiy media (Sayt/TV)|Ijtimoiy media (Telegram/Instagram)",
            Notes: "Yuqori tahrir nazorati, barqarorlik, ammo sekin tarqalish va yangilanishning murakkabligi.|Keng qamrov, tezkor tahrirlash imkoniyati, multimediaviylik va rasmiy maqom.|Maksimal tezlik, ikki tomonlama interaktiv muloqot, biroq nazoratsiz axborot oqimi.",
            Scenario: "Muassasada o'tkaziladigan tadbir vaqti kutilmaganda 2 soat oldinga surildi. Axborotni qaysi kanal orqali yetkazish eng to'g'ri?",
            Takeaway: "Tezkor o'zgarishlar zudlik bilan rasmiy ijtimoiy tarmoq kanali orqali e'lon qilinadi, doimiy manba sifatida rasmiy veb-saytda yangilanadi va stenddagi bosma e'lon zudlik bilan almashtiriladi.",
            LessonType: VisualLessonType.ComparisonMatrix,
            DiagramBadge: "KANALLAR MATRITsASI",
            CoreConcept: "Har bir media kanalining tezlik, ishonchlilik va qamrov ko'rsatkichlari turlicha.",
            InteractiveItems:
            [
                new("E'lonlar stendi (Bosma)", "Uzoq saqlanuvchan", "O'quv binosidagi har bir talaba ko'rishi mumkin, lekin tezkor o'zgarishni ko'rsatmaydi.", "Eski e'lonni yangisiga almashtirish", "#93C5FD"),
                new("Rasmiy veb-sayt (Elektron)", "Huquqiy asos", "Xabarning rasmiy va o'zgarmas arxivi sifatida xizmat qiladi.", "Yangilangan jadval havolasini saytga joylash", "#5EEAD4"),
                new("Kanal/Guruh (Ijtimoiy)", "Yuqori tezkorlik", "Foydalanuvchilar smartfoniga darhol bildirishnoma (push) boradi.", "Muhim e'lonni biriktirib (pin qilib) qo'yish", "#C4B5FD")
            ]
        ),

        // 3-MAVZU: Media namunalarini turi va vazifasiga ko‘ra tasniflash
        new(
            Title: "Media kontentining bosh vazifasini aniqlash spektri",
            Subtitle: "Axborot berish, o'rgatish, ko'ngil ochar va ishontirish/reklama chegaralari",
            Nodes: "Xolis xabar berish|Ta'lim berish / O'rgatish|Ishontirish va targ'ibot",
            Notes: "Faktlar, voqea joyi, vaqt va ishtirokchilar haqida neytral uslubdagi birlamchi axborot.|Qadamma-qadam yo'riqnoma, ko'nikma hosil qilish va bilimlarni mustahkamlashga qaratilgan kontent.|Fikrni o'zgartirish, mahsulotni sotish yoki muayyan harakatga undashga qaratilgan ta'sir.",
            Scenario: "Videoda noutbukni changdan tozalash qadamlari tushuntiriladi, lekin oxirgi 30 soniyada ma'lum tozalash vositasi do'koni qattiq tavsiya etiladi.",
            Takeaway: "Ushbu kontent gibrid (aralash) xarakterga ega: asosiy qismi o'quv-amaliy yo'riqnoma bo'lsa-da, yakuniy qismi tijoriy reklama hisoblanadi. Tomoshabin bu vositasiz ham ishni bajara olishini bilishi kerak.",
            LessonType: VisualLessonType.AnatomyDeconstruction,
            DiagramBadge: "FUNKSIONAL TASNIF",
            CoreConcept: "Bitta media mahsulot bir vaqtning o'zida ham ta'limiy, ham tijoriy maqsadni ko'zlashi mumkin.",
            InteractiveItems:
            [
                new("00:00 - 03:00 qism", "Ta'limiy blok", "Sovutish tizimini tozalash bo'yicha ketma-ketlik va ko'rsatmalar berilgan.", "Foydali amaliy texnika sifatida o'rganish", "#86EFAC"),
                new("03:00 - 03:30 qism", "Reklama integratsiyasi", "«Faqat X brendining spreyini ishlating, boshqalari xavfli» degan gap bor.", "Savdo chaqirig'ini xolis tavsiyadan ajratish", "#FDA4AF"),
                new("Yakuniy xulosa", "Ongli iste'mol", "Muallif homiy mahsulotini ko'rsatmoqda, bu tabiiy, lekin majburiy talab emas.", "Muqobil xavfsiz tozalash usullarini ham bilish", "#FDBA74")
            ]
        ),

        // 4-MAVZU: Axborot manbalari va qidiruv strategiyalari
        new(
            Title: "Aniq qidiruv so'rovini shakllantirish voronkasi",
            Subtitle: "Noaniq va umumiy so'rovlardan maqsadli natijaga bosqichma-bosqich o'tish",
            Nodes: "Muammo yadrosi|Texnik kontekst|Ishonchli manba filtri",
            Notes: "«Printer ishlamayapti» kabi haddan tashqari noaniq shikoyatdan aniq xatolik kodini ajrating.|Qurilmaning aniq modeli, operatsion tizim (Windows 11) va ulanish turini qo'shing.|Ishlab chiqaruvchining rasmiy qo'llanmasi (manual) yoki texnik bazasini ko'rsating.",
            Scenario: "HP LaserJet printeri chop etishdan to'xtadi va ekranda «Error 50.2 Fuser Error» kodi paydo bo'ldi. Qidiruv qanday bo'lishi kerak?",
            Takeaway: "Umumiy «printer buzildi» so'rovi minglab foydasiz forumlarni ochadi. Aniq «HP LaserJet M402 Error 50.2 manual fix» so'rovi esa 1-natijadanoq yechimni beradi.",
            LessonType: VisualLessonType.DecisionTree,
            DiagramBadge: "QIDIRUV VORONKASI",
            CoreConcept: "Qidiruv qanchalik aniq texnik parametrlarga ega bo'lsa, xatolik ehtimoli shunchalik kamayadi.",
            InteractiveItems:
            [
                new("Umumiy qidiruv", "Samarasiz (1 mln natija)", "«Printer chop qilmayapti nima qilish kerak» — juda ko'p umumiy maqolalar.", "So'rovga model va xatolik matnini kiritish", "#FDA4AF"),
                new("Kontekstli qidiruv", "O'rtacha aniqlik", "«HP LaserJet xato 50.2» — ancha yaqin, lekin havaskor videolar ham chiqadi.", "Rasmiy texnik yo'riqnomani filtrga qo'shish", "#FDBA74"),
                new("Professional so'rov", "Maksimal aniqlik", "«HP LaserJet M402 \"error 50.2\" fuser troubleshooting» — to'g'ridan-to'g'ri yechim.", "Rasmiy texnik qo'llanmadan foydalanish", "#5EEAD4")
            ]
        ),

        // 5-MAVZU: Axborotni saralash va manbani baholash mezonlari
        new(
            Title: "Manba ishonchliligini baholovchi CRAAP / SMART filtri",
            Subtitle: "Mualliflik vakolati, sanasi, dalillar bazasi va ob'ektivlik ko'rsatkichlari",
            Nodes: "Muallif va vakolat|Sana va dolzarblik|Dalillar va havolalar|Manfaat va betaraflik",
            Notes: "Maqola kim tomonidan yozilgan? Muallif soha mutaxassisimi yoki anonim blogermi?|Ma'lumot qachon e'lon qilingan va qachon yangilangan? Texnik sohada 5 yil oldingi fakt eskirgan bo'lishi mumkin.|Keltirilgan da'volarga rasmiy manbalar, tadqiqotlar va xalqaro havolalar bormi?|Muallifning tijoriy, siyosiy yoki shaxsiy manfaati bormi, neytral ohangda yozilganmi?",
            Scenario: "Blogda «Yangi noma'lum dastur kompyuterni 300% tezlashtiradi, barcha rasmiy drayverlarni o'chiring» deb yozilgan, muallifi noma'lum.",
            Takeaway: "Anonim muallif, asossiz da'volar va rasmiy drayverlarga qarshi tavsiyalar — bu zararli dastur (malware) yoki fishing xavfining yaqqol belgisidir.",
            LessonType: VisualLessonType.RedFlagsInspector,
            DiagramBadge: "MEZONLAR FILTRI",
            CoreConcept: "Har qanday da'vo mustaqil va tekshirilgan manbalar bilan quvvatlanishi shart.",
            InteractiveItems:
            [
                new("Muallif anonimligi", "Xavf darajasi: Yuqori", "Texnik tavsiya berayotgan shaxsning mutaxassisligi va ismi oshkor etilmagan.", "Rasmiy ishlab chiqaruvchi resursiga o'tish", "#FDA4AF"),
                new("Vaqt omili", "Dolzarblik tekshiruvi", "Dasturiy ta'minot bo'yicha tavsiyaning versiyalar bilan mosligi o'rganilmagan.", "Hujjatning oxirgi yangilangan sanasini ko'rish", "#FDBA74"),
                new("Fakt va dalil", "Isbotsiz da'vo", "300% tezlashtirish — benchmark testlari va texnik o'lchovlarsiz quruq gap.", "Haqiqiy test natijalari mavjudligini tekshirish", "#93C5FD")
            ]
        ),

        // 6-MAVZU: Kalit so‘z va filtrlar yordamida axborot izlash
        new(
            Title: "Kengaytirilgan qidiruv operatorlari va sintaksis xaritasi",
            Subtitle: "Google va boshqa qidiruv tizimlarida professional filtrlash usullari",
            Nodes: "Iborani qat'iy qidirish ( \" \" )|Sayt bo'yicha cheklash ( site: )|Format bo'yicha filtr ( filetype: )",
            Notes: "Qo'shtirnoq ichiga olingan kalit so'zlar aynan shu tartibda va o'zgarishsiz qidiriladi.|Qidiruv natijalarini faqat belgilangan domen (masalan edu.uz, gov.uz, cisco.com) bilan cheklaydi.|Faqat kerakli fayl turini: masalan PDF darslik, PPTX taqdimot yoki DOCX hujjatlarni topadi.",
            Scenario: "Talaba kiberxavfsizlik fani bo'yicha rasmiy OTM dasturi yoki PDF qo'llanmasini topishi kerak.",
            Takeaway: "«kiberxavfsizlik asoslari site:edu.uz filetype:pdf» so'rovi faqat OTM domenidagi haqiqiy PDF qo'llanmalarni bir necha soniyada saralab beradi.",
            LessonType: VisualLessonType.DecisionTree,
            DiagramBadge: "OPERATORLAR XARITASI",
            CoreConcept: "Maxsus operatorlar millionlab tasodifiy reklamali sahifalarni elakdan o'tkazadi.",
            InteractiveItems:
            [
                new("site:edu.uz filtri", "Domen cheklovi", "Natijalar faqat O'zbekiston oliy ta'lim tizimidagi rasmiy veb-saytlar ichidan chiqadi.", "Akademik ishonchlilik kafolati", "#5EEAD4"),
                new("filetype:pdf filtri", "Hujjat formati", "Veb-sahifalar, bloglar va yangiliklar chetlab o'tilib, tayyor kitob/qo'llanmalar ochiladi.", "Faqat yuklab olinadigan kitob va jurnallar", "#C4B5FD"),
                new("\"aniq ibora\" qo'shtirnoq", "Sintaksis filtri", "Mavzuga aloqasiz so'z o'yinlari va tasodifiy mosliklar bartaraf qilinadi.", "Aniq mavzu mundarijasi bo'yicha topish", "#86EFAC")
            ]
        ),

        // 7-MAVZU: Bir nechta manbani taqqoslash va vazifaga mosini tanlash
        new(
            Title: "Axborot manbalarini solishtirish va qaror qabul qilish",
            Subtitle: "Rasmiy hujjat, savdo tavsifi va foydalanuvchilar forumi qarama-qarshiligi",
            Nodes: "Ishlab chiqaruvchi rasmiy spetsifikatsiyasi|Sotuvchi katalogi va tavsifi|Foydalanuvchilar hamjamiyati / Forum",
            Notes: "Texnik chegaralar, maksimal qo'llab-quvvatlash va rasmiy kafolatlar uchun asosiy hujjat.|Tovarning narxi, to'plami va yetkazib berish shartlari. Tijoriy bo'yoqlar bo'lishi mumkin.|Haqiqiy foydalanish tajribasi, yashirin nuqsonlar va amaliy sinovlar haqida fikrlar.",
            Scenario: "Noutbukka qo'shimcha operativ xotira (RAM) o'rnatmoqchisiz. Turli manbalar 8 GB, 16 GB va 32 GB deb turlicha yozgan.",
            Takeaway: "Birinchi navbatda noutbuk anakartining rasmiy qo'llanmasi tekshiriladi. Forumdagi tajriba esa aynan qaysi RAM chastotasi yaxshi ishlashini aniqlashtirishda yordam beradi.",
            LessonType: VisualLessonType.ComparisonMatrix,
            DiagramBadge: "TRIANGULYaTSIYa",
            CoreConcept: "Muhim qaror qabul qilishda kamida uchta mustaqil manba o'zaro solishtirilishi lozim.",
            InteractiveItems:
            [
                new("Rasmiy PDF Manual", "Eng yuqori ustuvorlik", "Anakart cheklovlari va maksimal quvvat aniq ko'rsatilgan.", "Birlamchi texnik mezon qilib olish", "#5EEAD4"),
                new("Do'kon sahifasi", "Ikkinchi darajali", "Ko'pincha boshqa modellar ma'lumoti bilan adashtirib nusxalangan bo'lishi mumkin.", "Parametrlarni qo'llanma bilan solishtirish", "#FDBA74"),
                new("Reddit / 4PDA forumi", "Amaliy tajriba", "Haqiqiy foydalanuvchilar qaysi brend RAM bilan barqaror ishlaganini yozadi.", "Haqiqiy sinovlar va muvaffaqiyatli keyslar", "#93C5FD")
            ]
        ),

        // 8-MAVZU: Media xabarining muallifi, maqsadi va auditoriyasi
        new(
            Title: "Media xabarining ichki anatomiyasi va 3D koordinatasi",
            Subtitle: "Har qanday media xabarining orqasida kim turibdi, kimga yo'naltirilgan va nima maqsadda yaratilgan?",
            Nodes: "Muallif / Tashkilot|Maqsadli auditoriya|Ko'zlangan yakuniy natija",
            Notes: "Xabarni kim yaratdi va kim moliyalashtirdi? Muallifning obro'si va sohaga aloqadorligi qanday?|Xabar kimning yoshi, kasbi, qiziqishi yoki ehtiyojiga moslashtirilgan? Ohang va visual uslub kimga mos?|O'quvchida qanday hissiyot uyg'otish va uni qanday amaliy harakatga undash ko'zlangan?",
            Scenario: "«Ustaxonamizda faqat bugun barcha talabalar uchun bepul diagnostika va sovg'alar!» degan yorqin e'lon paydo bo'ldi.",
            Takeaway: "E'lon ustaxona tomonidan talabalar auditoriyasiga yo'naltirilgan. Maqsad bepul xizmat ko'rsatish bahonasida yangi doimiy mijozlarni jalb qilish va pullik ta'mirlash xizmatini sotishdir.",
            LessonType: VisualLessonType.AnatomyDeconstruction,
            DiagramBadge: "XABAR ANATOMIYaSI",
            CoreConcept: "Media mahsulotlarining hech biri tasodifiy yaratilmaydi — ularning aniq maqsadi bor.",
            InteractiveItems:
            [
                new("Muallif: Xususiy servis", "Tijoriy tashkilot", "Nodavlat xususiy biznes o'z xizmatlarini ommalashtirishga intilmoqda.", "Tijoriy manfaat borligini inobatga olish", "#93C5FD"),
                new("Auditoriya: Talabalar", "Budjeti cheklangan qatlam", "«Bepul», «Talabalar uchun» so'zlari orqali yoshlarning e'tibori tortilmoqda.", "Qo'shimcha yashirin to'lovlar shartini bilish", "#C4B5FD"),
                new("Niyat: Mijozlar bazasi", "Marketin strategiyasi", "«Faqat bugun» shoshiltirish usuli tezda tashrif buyurishga undaydi.", "Shoshmasdan haqiqiy shartlarni o'rganish", "#FDA4AF")
            ]
        ),

        // 9-MAVZU: Feyk xabar, dezinformatsiya va manipulyatsiya belgilari
        new(
            Title: "Manipulyatsiya va dezinformatsiyaning «Qizil bayroqlari»",
            Subtitle: "Feyk yangiliklar va yolg'on axborotlarning asosiy diagnostik belgilari",
            Nodes: "Hissiy bosim va shoshiltirish|Kontekstdan uzilgan faktlar|Asossiz va radikal da'volar",
            Notes: "«Shoshiling», «Darhol tarqating», «Hamma bilsin» kabi vahimali iboralar tanqidiy fikrlashni bloklaydi.|Eski video yoki fotosuratlar yangi hodisa sifatida taqdim etiladi, sanalar ataylab yashiriladi.|«Dunyodagi hamma olimlar tan oldi», «100% barcha kompyuterlar buzildi» kabi radikal umumlashtirishlar.",
            Scenario: "«DIQQAT! Telegram bugun tunda barcha akkauntlarni o'chiradi, faqat ushbu xabarni 10 kishiga yuborsangiz hisobingiz saqlanib qoladi!»",
            Takeaway: "Bu klassik manipulyativ spamlardan biri. Messenjerlar tizimli o'zgarishlarni rasmiy xabarnoma orqali yuboradi, foydalanuvchilar orqali zanjir hosil qilmaydi.",
            LessonType: VisualLessonType.RedFlagsInspector,
            DiagramBadge: "FEYK-DETEKTOR",
            CoreConcept: "Hissiyotlar qanchalik kuchli qo'zg'atilsa, faktik asos shunchalik kam bo'ladi.",
            InteractiveItems:
            [
                new("Shoshiltirish chaqirig'i", "Qizil bayroq #1", "«Bugun tunda», «Zudlik bilan» — tekshirishga vaqt qoldirmaslik taktikasi.", "To'xtang, nafas oling va xabarni tarqatmang", "#FDA4AF"),
                new("Zanjirli tarqatish talabi", "Qizil bayroq #2", "«10 kishiga yuboring» — bu tarmoq botlari va spam mexanizmi belgisi.", "Guruh va suhbatdoshlarga yubormaslik", "#FDBA74"),
                new("Rasmiy manba yo'qligi", "Qizil bayroq #3", "Telegram rasmiy blogi yoki ilova ichidagi yangiliklarda bunday e'lon yo'q.", "Ilovaning rasmiy kanalini tekshirish", "#93C5FD")
            ]
        ),

        // 10-MAVZU: Xabarni asl manba, sana va mustaqil dalillar orqali tekshirish
        new(
            Title: "Faktcheking va dalillar zanjirini tekshirish protokoli",
            Subtitle: "Shubhali xabarlarni birlamchi manbaga qadar kuzatib borish algoritmi",
            Nodes: "Da'vo matnini qat'iy ajrating|Birlamchi muallif yoki hujjatni toping|Mustaqil dalillar bilan kesishuvni tekshiring",
            Notes: "Skrinshot yoki qayta uzatilgan xabardagi da'voni ajratib, qidiruv tizimiga matn qilib kiriting.|Xabar birinchi marta qayerda, kim tomonidan e'lon qilingan? Dastlabki post sanasini aniqlang.|Bitta manbadan ko'chirilgan 10 ta postni 10 ta mustaqil dalil deb o'ylamang, turli manbalarni toping.",
            Scenario: "Talabalar guruhida «Vazirlik qarori bilan qishki ta'til 1 oyga uzaytirildi» degan buyruq qog'ozi skrinshoti tarqaldi.",
            Takeaway: "Skrinshotlar Photoshop orqali oson soxtalashtiriladi. Haqiqiy me'yoriy hujjat Lex.uz portalida yoki vazirlikning tasdiqlangan rasmiy veb-saytida raqami bilan e'lon qilinishi shart.",
            LessonType: VisualLessonType.EvidenceVerification,
            DiagramBadge: "FAKTChEKING PROTOKOLI",
            CoreConcept: "Rasmiy muhr va skrinshot dalil emas; qonuniy kuchga ega portal havolasi dalildir.",
            InteractiveItems:
            [
                new("Skrinshot tahlili", "Vizual tekshiruv", "Shriftlar har xil, sanada noaniqlik va buyruq raqami xira ko'rinmoqda.", "Hujjat raqamini qidiruvga kiritish", "#FDBA74"),
                new("Lex.uz portali", "Qonuniy reyestr", "O'zbekiston Respublikasi qonunchilik ma'lumotlari milliy bazasida bunday qaror yo'q.", "Faqat qonuniy kuchga ega hujjatlarga ishonish", "#5EEAD4"),
                new("Rasmiy matbuot xizmati", "Yakuniy xulosa", "Vazirlik matbuot kotibi xabarni feyk deb rasman rad etdi.", "Guruh a'zolariga raddiyani e'lon qilish", "#86EFAC")
            ]
        ),

        // 11-MAVZU: Post, reklama yoki videoni tanqidiy tahlil qilish
        new(
            Title: "Reklama va targ'ibot roliklarini tahlil qilish dekoderi",
            Subtitle: "Yashirin ta'sir usullari, hissiy manipulyatsiya va haqiqiy faktlar chegarasi",
            Nodes: "Tekshiriluvchi da'vo|Ko'rsatilgan faktik dalillar|Qo'llanilgan psixologik ta'sir vositalari",
            Notes: "Rolik tomoshabinga aniq nima va'da qilmoqda? Natija o'lchanadimi (masalan, vaqt, pul, tezlik)?|Ekranda haqiqiy laboratoriya sinovlari yoki mustaqil o'lchovlar ko'rsatilganmi yoki shunchaki rol o'ynalganmi?|Dinamik musiqa, mashhur yulduzlar ishtiroki, qo'rquv yoki havas uyg'otish usullari qanday ishlatilgan?",
            Scenario: "Videoda eski noutbukka bir tugmali tozalagich dastur o'rnatilgach, u eng zamonaviy o'yinlarni qotmasdan ochgani ko'rsatiladi.",
            Takeaway: "Kompyuterning apparat quvvati (CPU, GPU, RAM) dasturiy tozalagich bilan fizik jihatdan o'smaydi. Videodagi montaj va tezlashtirilgan kadrlar tomoshabinni aldash uchun xizmat qiladi.",
            LessonType: VisualLessonType.AnatomyDeconstruction,
            DiagramBadge: "REKLAMA DEKODERI",
            CoreConcept: "Reklamadagi vizual effektlar texnik imkoniyatlar faktini o'zgartira olmaydi.",
            InteractiveItems:
            [
                new("Kadrlar montaji", "Vizual aldov", "O'yin boshqa kuchli kompyuterda yoqilib, eski monitorga tasvir uzatilgan bo'lishi mumkin.", "Texnik testlarni mustaqil manbalarda ko'rish", "#FDA4AF"),
                new("Hissiy va'dalar", "Mo''jizaviy yechim", "Eski texnikani yangisidek qilish da'vosi fizik qonunlarga zid.", "Qurilmaning apparat cheklovlarini hisobga olish", "#FDBA74"),
                new("Xolis xulosa", "Ehtiyotkorlik", "Dastur faqat keshni tozalashi mumkin, ammo apparat tezligini oshira olmaydi.", "Zararli tozalagichlarni o'rnatmaslik", "#86EFAC")
            ]
        ),

        // 12-MAVZU: Media mahsulotini rejalash va ssenariy tuzish
        new(
            Title: "Media kontent yaratishning 3 bosqichli rejissyorlik xaritasi",
            Subtitle: "G'oyadan tortib tayyor ssenariy va kadrlar rejasigacha (Storyboard)",
            Nodes: "01. Kirish (Hook) — 0-5 sek|02. Asosiy yadro — 6-45 sek|03. Xulosa va Harakatga chaqiriq (CTA)",
            Notes: "Tomoshabin e'tiborini birinchi soniyalardanoq jalb qiluvchi muammo yoki qiziqarli savol qo'ying.|Mavzuning mohiyatini 2-3 ta aniq amaliy qadamda vizual tarzda ko'rsating va tushuntiring.|Ko'rilgan mavzudan qanday xulosa chiqarish kerak va tomoshabin nima qilishi kerakligini ayting.",
            Scenario: "Talabalarga «Kompyuter klaviaturasini to'g'ri tozalash» mavzusida 1 daqiqali qisqa video (Reels/Shorts) tayyorlash kerak.",
            Takeaway: "Kirishda kir klaviatura ko'rsatiladi va xavflar aytiladi. O'rtada xavfsiz tozalash usuli ko'rsatiladi. Yakunda esa klaviaturani profilaktika qilish maslahati beriladi.",
            LessonType: VisualLessonType.StoryboardPipeline,
            DiagramBadge: "SSENARIY KASKADI",
            CoreConcept: "Qisqa videolarda har bir soniya hisobda: vizual va ovozli qator bir-birini to'ldirishi kerak.",
            InteractiveItems:
            [
                new("Hook (Ilmoq)", "00:00 - 00:05", "«Klaviaturangizda mikroblar smartfon ekranidagidan ko'proq!» kadr va muammo.", "Tomoshabin e'tiborini ushlab qolish", "#5EEAD4"),
                new("Demonstratsiya", "00:05 - 00:45", "Siqilgan havo va mikrofibra mato bilan tozalash ketma-ketligi ko'rsatiladi.", "Aniq amallarni yaqin planda namoyish etish", "#93C5FD"),
                new("Call to Action (CTA)", "00:45 - 00:60", "«Bu videoni kompyuterda ko'p ishlaydigan do'stingizga yuboring!»", "Auditoriyani ijobiy harakatga undash", "#C4B5FD")
            ]
        ),

        // 13-MAVZU: Matn, tasvir va videoni auditoriyaga mos taqdim etish
        new(
            Title: "Auditoriya xususiyatlari va media formatlari muvofiqligi",
            Subtitle: "Qachon matn, qachon infografika va qachon video formatini tanlash lozim?",
            Nodes: "Matnli qo'llanma (Tekst/PDF)|Sxematik infografika (Tasvir)|Video / Animatsiya yo'riqnoma",
            Notes: "Qadamlar ko'p bo'lganda, kodlar yoki qidiruv talab etilganda, sekin o'qib o'rganish uchun eng qulay.|Tizim arxitekturasi, statistik taqqoslashlar va bir qarashda tushuniladigan xulosalar uchun.|Jismoniy harakatlar, dastur interfeysida tugmalarni bosish ketma-ketligini ko'rsatish uchun.",
            Scenario: "Yangi talabalarga Wi-Fi tarmog'iga ulanish va login-parol kiritish tartibini tushuntirish kerak.",
            Takeaway: "Eng ma'qul yechim — 3 qadamli vizual infografika plakatini Wi-Fi zonalariga ilib qo'yish va batafsil sozlamalar uchun QR kod orqali matnli qo'llanmaga havola berish.",
            LessonType: VisualLessonType.AudienceFormatMatrix,
            DiagramBadge: "FORMATLAR MOSLIGI",
            CoreConcept: "Format foydalanuvchining vaziyatiga (oflayn, harakatda, chuqur o'rganish) qarab tanlanadi.",
            InteractiveItems:
            [
                new("Infografika plakati", "Birlamchi tezkor aloqa", "3 ta qadam: 1. Tarmoqni tanlang, 2. Saytga kiring, 3. Talaba ID kiriting.", "Devor va stendlarga joylashtirish", "#86EFAC"),
                new("QR kodli PDF", "Batafsil ma'lumot", "Telefon sozlamalarida xatolik chiqsa nima qilish kerakligi haqida qo'llanma.", "Muammoli holatlarda o'qish uchun", "#93C5FD"),
                new("Qisqa video", "Qo'shimcha format", "Ekran yozuvi orqali kirish jarayonini ko'rsatuvchi 30 soniyali animatsiya.", "Ijtimoiy guruhlarda tarqatish", "#FDBA74")
            ]
        ),

        // 14-MAVZU: Matnli va tasvirli kontent yoki infografika yaratish
        new(
            Title: "Professional infografika va vizual kompozitsiya qoidalari",
            Subtitle: "Vizual ierarxiya, ranglar kontrasti va axborot yuklamasini me'yorlashtirish",
            Nodes: "Fokus sarlavha va kirish|Vizual ierarxiya va piktogrammalar|Faktik manba va mualliflik belgisi",
            Notes: "Infografika nima haqida ekanligi 2 soniyada tushunilishi lozim. Sarlavha katta va aniq bo'lsin.|Muhim ma'lumotlar katta hajmda, ikkinchi darajalisi kichikroq bo'lsin. Bo'shliq (white space) qoldiring.|Har bir raqam va grafika ostida ma'lumot qayerdan olinganligi (manba) ko'rsatilishi shart.",
            Scenario: "O'quv xonasida «Kompyuter xavfsizligining 5 ta oltin qoidasi» nomli didaktik plakat tayyorlanmoqda.",
            Takeaway: "Plakat matnga to'lib ketmasligi kerak. 5 ta aniq belgi (icon), qisqa tushunarli matn va yuqori kontrastli zamonaviy ranglar auditoriyaning eslab qolishini 80% ga oshiradi.",
            LessonType: VisualLessonType.InfographicChecklist,
            DiagramBadge: "VIZUAL DIZAYN STANDARTI",
            CoreConcept: "Yaxshi infografika murakkab ma'lumotni soddalashtiradi, uni o'qish emas, tushunish oson bo'ladi.",
            InteractiveItems:
            [
                new("Shrift va o'lcham", "Ierarxiya", "Sarlavha 32pt, bandlar 18pt, tushuntirishlar 14pt — ko'z tartib bilan o'qiydi.", "Matnlar orasidagi masofani saqlash", "#5EEAD4"),
                new("Rang balansi", "Kontrast talabi", "Qorong'i fonda och matnlar, xavf signallari uchun iliq to'q sariq/qizil ranglar.", "Ko'zni toliqtirmaydigan palitradan foydalanish", "#FDBA74"),
                new("Belgilar (Icons)", "Assotsiativ qabul", "Qulf belgisi — maxfiylik, qalqon — himoya, ko'z — kuzatuv ma'nosini tez bildiradi.", "Matn o'rniga mos piktogrammalarni tanlash", "#C4B5FD")
            ]
        ),

        // 15-MAVZU: Qisqa videoxabar tayyorlash va taqdim etish
        new(
            Title: "Mobil videoxabar yaratishning texnik xavfsizlik nazorati",
            Subtitle: "Suratga olishdan oldingi, jarayondagi va montajdan keyingi tekshiruvlar",
            Nodes: "Sahnani tayyorlash va tekshirish|Ovoz, yorug'lik va kadr barqarorligi|Kadr ortidagi maxfiylik va tahrir",
            Notes: "Kadrga begona odamlar, shaxsiy hujjatlar yoki maxfiy monitorlar tushib qolmasligini ko'ring.|Yuzga yorug'lik to'g'ri tushsin, mikrofon shamol va shovqindan himoyalangan bo'lsin.|Videoda tasodifan mijoz telefoni yoki parol ko'ringan bo'lsa, xiralashtiring (blur) yoki qayta oling.",
            Scenario: "Kollej ustaxonasida dastgohni sozlash bo'yicha video olindi, ammo orqa fondagi doskada o'qituvchining paroli yozilgani ko'rindi.",
            Takeaway: "Bunday videoni tarmoqqa yuklash qat'iyan man etiladi. Parol ko'ringan kadrni montajda qirqib tashlash, xiralashtirish yoki sahnani tozalab qayta suratga olish shart.",
            LessonType: VisualLessonType.RedFlagsInspector,
            DiagramBadge: "PRODYUSER ChEK-LISTI",
            CoreConcept: "Video sifatini faqat kamera darajasi emas, balki kadr xavfsizligi va axborot madaniyati belgilaydi.",
            InteractiveItems:
            [
                new("Kadr orqa foni", "Xavfsizlik tekshiruvi", "Doskadagi yozuvlar, monitor parollari, mijozlarning shaxsiy raqamlari.", "Kadr maydonini begona yozuvlardan tozalash", "#FDA4AF"),
                new("Ovoz tozaligi", "Sifat talabi", "Shovqinli ustaxonada петличка (lavalier) mikrofondan foydalanish.", "Ovoz balandligini me'yorida sozlash", "#93C5FD"),
                new("Yakuniy ko'rik", "Pre-flight nazorat", "Video eksport qilingach, to'liq ekranda boshdan-oyoq qayta ko'riladi.", "Barcha xatolarni yuklashdan oldin tuzatish", "#86EFAC")
            ]
        ),

        // 16-MAVZU: Media etikasi va netiket
        new(
            Title: "Professional netiket va ziddiyatsiz onlayn muloqot",
            Subtitle: "Destruktiv muloqotdan konstruktiv hamkorlikka o'tish modeli",
            Nodes: "Vaziyatni shaxsiylashtirmay bayon etish|Hurmat bilan aniqlashtiruvchi savol berish|Keyingi amaliy qadam va muddatni ko'rsatish",
            Notes: "«Siz tushunmaysiz», «o'zingiz aybdorsiz» demasdan, muammoni texnik fakt sifatida ajrating.|Qarshi tomonning his-tuyg'ularini kamsitmay, aniq yetishmayotgan faktlarni so'rang.|Muammo qachon, qanday tartibda ko'rib chiqilishini aniq va xushmuomala yozing.",
            Scenario: "Foydalanuvchi jamoat guruhida: «Sizlarning dasturingiz bir tiyinga qimmat, hech narsa ishlamayapti!» deb haqoratli xabar qoldirdi.",
            Takeaway: "Haqoratga haqorat bilan javob berish obro'ni to'kadi. Xolis va xotirjamlik bilan xatolik qaysi bosqichda chiqqanini aniqlash va yordam taklif qilish professional standartdir.",
            LessonType: VisualLessonType.NetiquetteDialogue,
            DiagramBadge: "NETIKET STANDARTI",
            CoreConcept: "Muloqot madaniyati shaxsiy his-tuyg'ular ustidan aql va kasbiy etikaning ustunligidir.",
            InteractiveItems:
            [
                new("Salbiy reaksiya (Noto'g'ri)", "Ziddiyatni kuchaytiradi", "«O'zingiz ishlatishni bilmaysiz, noshud!» — jamoat oldida nizo keltirib chiqaradi.", "Hissiyotga berilmasdan vazmin qolish", "#FDA4AF"),
                new("Etik javob (To'g'ri)", "Vaziyatni yumshatadi", "«Assalomu alaykum. Noqulaylik uchun uzr so'raymiz. Xatolik aynan qaysi oynada chiqdi?»", "Muammoga diqqatni qaratish", "#86EFAC"),
                new("Yechim yo'li", "Natijaga yo'naltirilgan", "«Skrinshotni bizning qo'llab-quvvatlash botimizga yuborsangiz, darhol ko'rib chiqamiz».", "Muloqotni professional o'zanga burish", "#5EEAD4")
            ]
        ),

        // 17-MAVZU: Mualliflik huquqi, plagiat va manba ko‘rsatish
        new(
            Title: "Raqamli materiallardan qonuniy foydalanish va litsenziyalar",
            Subtitle: "Public Domain, Creative Commons (CC) va All Rights Reserved chegaralari",
            Nodes: "Mualliflik huquqi bilan to'liq himoyalangan|Creative Commons (CC litsenziyalari)|Jamoat mulki (Public Domain) va Ochiq manba",
            Notes: "Muallifning yozma ruxsatisiz tijoriy yoki ochiq loyihalarda foydalanish qat'iyan taqiqlanadi.|Erkin foydalanish mumkin, ammo muallif ismini ko'rsatish (BY) yoki notijoriy (NC) shartlari bor.|Cheklovlarsiz hamma uchun ochiq, muddatlari o'tgan yoki muallif o'z huquqlaridan voz kechgan.",
            Scenario: "Dars taqdimotiga Google rasmlar qidiruvidan topilgan chiroyli dizaynni joylamoqchisiz.",
            Takeaway: "Google qidiruvidagi barcha rasmlar avtomatik erkin degani emas. Maxsus «Foydalanish huquqi -> Creative Commons litsenziyasi» filtri orqali qidirish va muallifni ko'rsatish shart.",
            LessonType: VisualLessonType.CopyrightLicenseGuide,
            DiagramBadge: "LITSENZIYa XARITASI",
            CoreConcept: "Birovning intellektual mehnatidan ruxsatsiz foydalanish qonuniy va axloqiy qoidabuzarlikdir.",
            InteractiveItems:
            [
                new("CC-BY (Attribution)", "Erkin foydalanish", "Materialni o'zgartirish va ishlatish mumkin, faqat muallif nomi ko'rsatilsa bas.", "Rasm tagiga muallif nomini yozish", "#86EFAC"),
                new("CC-BY-NC (Non-Commercial)", "Faqat notijoriy", "Faqat ta'lim va notijoriy maqsadlarda ishlatish mumkin, sotish taqiqlanadi.", "Tijoriy loyihalarda foydalanmaslik", "#FDBA74"),
                new("All Rights Reserved", "Qat'iy taqiq", "Muallifning rasmiy yozma roziligisiz har qanday nusxalash noqonuniy hisoblanadi.", "Foydalanish uchun ruxsat olish yoki almashtirish", "#FDA4AF")
            ]
        ),

        // 18-MAVZU: Shaxsiy ma’lumotlar, raqamli gigiyena va kiberbulling
        new(
            Title: "Raqamli xavfsizlik va shaxsiy daxlsizlik qalqoni",
            Subtitle: "Hisoblar himoyasi, ikki bosqichli autentifikatsiya (2FA) va kiberbullingdan himoya",
            Nodes: "Kuchli parollar va 2FA himoyasi|Profil ma'lumotlari minimallashuvi|Kiberbullingga nisbatan qat'iy javob",
            Notes: "Har bir xizmat uchun noyob parol yarating va albatta SMS/Authenticator orqali 2FA ni yoqing.|Manzil, telefon, pasport nusxasi kabi maxfiy ma'lumotlarni ochiq tarmoqlarga aslo joylamang.|Haqoratli xabarlarga javob qaytarmang, skrinshot qilib dalil sifatida saqlang va bloklang.",
            Scenario: "Guruhda notanish foydalanuvchi: «Sizga kutilmagan mukofot chiqdi, uni olish uchun profilingizga kelgan 6 xonali SMS kodni ayting» deb yozdi.",
            Takeaway: "SMS kod — bu sizning akkauntingiz kaliti. Hech qanday kompaniya yoki xodim sizdan bir martalik tasdiqlash kodini so'rashga haqli emas. Kodni berish hisobni o'g'irlatish demakdir.",
            LessonType: VisualLessonType.DigitalHygieneShield,
            DiagramBadge: "XAVFSIZLIK QALQONI",
            CoreConcept: "Kiberxavfsizlik murakkab dasturlardan emas, insonning hushyorligi va raqamli gigiyenasidan boshlanadi.",
            InteractiveItems:
            [
                new("SMS / 2FA kodlari", "Maksimal maxfiylik", "Bir martalik kodlarni hech kimga, hatto yaqin do'stlarga ham aytib bo'lmaydi.", "Kodni begonalarga oshkor qilmaslik", "#FDA4AF"),
                new("Notanish havolalar", "Fishing xavfi", "«Mukofot yutib oldingiz» kabi havolalar parollarni o'g'irlash uchun mo'ljallangan.", "Shubhali havolalarni bosmaslik", "#FDBA74"),
                new("Bloklash va shikoyat", "To'g'ri javob choralari", "Bunday hisoblar ustidan messenjer ma'muriyatiga «Spam/Firibgarlik» shikoyatini yuboring.", "Profilni darhol qora ro'yxatga kiritish", "#5EEAD4")
            ]
        ),

        // 19-MAVZU: Media mahsulotiga iqtibos va manba havolalarini to‘g‘ri kiritish
        new(
            Title: "Akademik halollik va manbalarni rasmiylashtirish standarti",
            Subtitle: "Matn ichidagi iqtiboslar, rasm osti izohlari va yakuniy bibliografiya",
            Nodes: "To'g'ridan-to'g'ri iqtibos ( \"...\" )|Parafraz va g'oya manbasi|Yakuniy manbalar ro'yxati (APA/IEEE)",
            Notes: "Birovning gapi o'zgarishsiz olinganda qo'shtirnoqqa olinadi va qavs ichida muallif/yil ko'rsatiladi.|Birovning fikrini o'z so'zingiz bilan qayta bayon qilsangiz ham, g'oya muallifiga havola berish shart.|Taqdimot yoki maqola oxirida barcha manbalarning to'liq veb-manzillari va nashr ma'lumotlari beriladi.",
            Scenario: "Taqdimot tayyorlashda internetdagi ilmiy maqoladan bitta jumlani ko'chirib oldingiz va slaydda manbasiz qoldirdingiz.",
            Takeaway: "Bu yashirin plagiat hisoblanadi. Matnni qo'shtirnoqqa olib, slayd burchagiga «Manba: Karimova D., 2024» deb kichik shriftda ko'rsatish akademik madaniyat belgisidir.",
            LessonType: VisualLessonType.CopyrightLicenseGuide,
            DiagramBadge: "HALOLLIK STANDARTI",
            CoreConcept: "Manbani aniq ko'rsatish ishingizning nufuzini tushirmaydi, aksincha uning ishonchliligini oshiradi.",
            InteractiveItems:
            [
                new("Matn ichidagi havola", "Iqtibos madaniyati", "«...ta'limda media savodxonlik muhim omildir» (Yo'ldoshev, 2025).", "Muallif va yilni ko'rsatish", "#5EEAD4"),
                new("Rasm osti imzosi", "Vizual kredit", "Grafika tagiga kichik shriftda: «Foto: UNESCO rasmiy arxivi» deb yozish.", "Vizual materiallar muallifini e'tirof etish", "#C4B5FD"),
                new("Havolalar faolligi", "Tekshiruvchanlik", "Oxirgi slaydda berilgan URL havolalar haqiqatan ishlayotganini tekshirish.", "Buzilgan yoki noto'g'ri havolalarni tuzatish", "#86EFAC")
            ]
        ),

        // 20-MAVZU: Maxfiylik va onlayn muloqotga oid vaziyatlarni yechish
        new(
            Title: "Kiberinsident va maxfiylik sizib chiqqanda tezkor chora-tadbirlar",
            Subtitle: "Shaxsiy yoki tashkiliy ma'lumotlar tarmoqqa chiqib ketganda 3 bosqichli protokol",
            Nodes: "01. Tarqalishni darhol to'xtating|02. Mas'ullarga va jabrlanuvchiga bildiring|03. Voqeani hujjatlashtiring va xulosa qiling",
            Notes: "Xabarni boshqa guruhlarga yubormang. O'chirish imkoni bo'lsa barcha foydalanuvchilar uchun o'chiring.|Tashkilot xavfsizlik mutaxassisi yoki rahbariyatga xavfsiz ichki kanal orqali tezda xabar bering.|Qanday ma'lumot sizib chiqqani, qancha vaqt ochiq turgani va ko'rilgan choralarni qayd eting.",
            Scenario: "Xodim mijozning pasport nusxasi va to'lov kvitansiyasini tasodifan 500 kishilik umumiy ochiq guruhga yuborib qo'ydi.",
            Takeaway: "1-soniyadanoq xabarni «Hamma uchun o'chirish» tugmasini bosish, guruh administratoridan keshni tozalashni so'rash va jabrlangan mijozni ogohlantirib, xavfsizlik choralarini ko'rish shart.",
            LessonType: VisualLessonType.DecisionTree,
            DiagramBadge: "INSIDENT PROTOKOLI",
            CoreConcept: "Xatoni tezda tan olish va to'g'ri bartaraf etish kelib chiqadigan zararni 90% ga kamaytiradi.",
            InteractiveItems:
            [
                new("Zudlik bilan o'chirish", "1-bosqich: Cheklash", "«Faqat o'zimdan o'chirish» emas, albatta «Barcha a'zolardan o'chirish» tanlanadi.", "Fayl tarqalishini zudlik bilan to'xtatish", "#FDA4AF"),
                new("Administratorga murojaat", "2-bosqich: Yordam", "Guruh ma'muri orqali xabarning replika yoki botlar orqali ko'chirilishini tekshirish.", "Texnik izlarni tozalash", "#FDBA74"),
                new("Sabablarini tahlil qilish", "3-bosqich: Profilaktika", "Maxfiy hujjatlarni jamoat chatlari orqali yuborishni taqiqlovchi ichki tartib joriy etish.", "Kelgusida bunday holatga yo'l qo'ymaslik", "#5EEAD4")
            ]
        )
    ];

    internal static readonly string[] WorkedExamples =
    [
        "Kuzatuv — Chatda sekinlik haqida xabar bor. Tezlik hali o‘lchanmagan.|Talqin — «Router nosoz» sabab haqidagi taxmin. Xabar buni dalillamaydi.|Tekshiruv — Simli va Wi-Fi ulanish tezligini bir xil sharoitda o‘lchang. Natijani avvalgi o‘lchov bilan solishtiring.",
        "Tezkor xabar — Rasmiy guruhda yangi vaqt, sana va laboratoriya raqami beriladi.|Barqaror ma’lumot — Jadval saytda ham yangilanadi va guruh xabarida unga havola beriladi.|Eski nusxa — Stenddagi avvalgi jadval yangisiga almashtiriladi.",
        "Ko‘rsatma — Videoning asosiy qismi printerni sozlash ketma-ketligini namoyish qiladi.|Tijorat qismi — Oxiridagi servis taklifi tomoshabinni xizmatga murojaat qilishga undaydi.|Tasnif — Video shaklan audiovizual, vazifasiga ko‘ra asosan o‘quv materialidir.",
        "Umumiy so‘rov — «Printer ishlamayapti» turli nosozliklarni aralashtirib yuboradi.|Aniq so‘rov — «[printer modeli] Paper Jam qo‘llanma» model va alomatni birlashtiradi.|Natija tanlovi — Qo‘llanma muqovasidagi model kodi qurilmadagi kod bilan solishtiriladi.",
        "Rasmiy yordam sahifasi — Model va operatsion tizim versiyasi ko‘rsatilgan.|Noma’lum blog — Tavsiya mavjud, lekin uning sanasi va dastur manbasi aniqlanmagan.|Qaror — Aynan shu qurilmaga tegishli yo‘riqnoma tanlanadi va sababi yoziladi.",
        "Boshlang‘ich so‘rov — Qurilma nomi natijalar orasiga boshqa modellarni ham olib kiradi.|Toraytirilgan so‘rov — To‘liq model kodi, ishlab chiqaruvchi sayti va PDF formati qo‘shiladi.|Hujjatni tekshirish — PDF ichida model kodi va hujjatning amal qilish doirasi ko‘riladi.",
        "Moslik — Qo‘llanmadan xotira turi va maksimal hajm olinadi.|Mahsulot — Sotuvchining kod, hajm va tur haqidagi ma’lumoti qo‘llanma bilan solishtiriladi.|Tajriba — Forumdagi misol faqat xuddi shu modelga tegishli bo‘lsa qo‘shimcha dalil sifatida ishlatiladi.",
        "Muallif — Xizmat taklifini ustaxona e’lon qilgan.|Auditoriya — Qurilmasida nosozlik bor va tekshiruv uchun joy izlayotgan mijozlar.|Ta’sir — «Faqat bugun» tezkor qarorga undaydi. Diagnostika doirasi e’lon shartlaridan tekshiriladi.",
        "Va’da — «Ikki baravar tez» iborasi o‘lchanadigan da’vo.|Yetishmayotgan tafsilot — Qaysi vazifa, qurilma va o‘lchov ishlatilgani aytilmagan.|Xolis bayon — Natija faqat ko‘rsatilgan sinovda qayd etilgan bo‘lsa, xabar shu sharoit bilan cheklanadi.",
        "Da’vo — Ertangi darslar onlayn bo‘lishi aytilgan.|Asl e’lon — Texnikumning rasmiy kanali, e’lon vaqti va guruh raqami tekshiriladi.|Tasdiqlash — Rasmiy jadval yoki mas’ul xodimning mustaqil tasdig‘i olinmaguncha skrinshot qayta ulashilmaydi.",
        "Kadrlar — Birinchi videoda dastur ochilishi sekin, ikkinchisida tez ko‘rsatilgan.|O‘lchov — Bir xil dastur va ish sharoitida ishga tushish vaqti solishtiriladi.|Talqin — Montaj yoki tezlashtirilgan tasvir sinov natijasi sifatida qabul qilinmaydi.",
        "00–10 soniya — Vazifa va inventar raqami nima uchun kerakligi aytiladi.|10–45 soniya — Raqam joylashgan yorliq yaqin planda ko‘rsatiladi.|45–60 soniya — Raqamning qaysi maydonga yozilishi namoyish etiladi.",
        "Boshlovchi uchun — Port tasvirida kabel kiradigan joy strelka bilan belgilanadi.|Usta uchun — Port turi, mos kabel va diagnostika tafsilotlari matnda beriladi.|Format tanlovi — Ulanish harakatini tushuntirish uchun qisqa video ham qo‘shilishi mumkin.",
        "Sarlavha — «Hisobingizni himoya qilishning uch amali» asosiy mazmunni bildiradi.|Uch belgi — Noyob parol, qo‘shimcha tasdiqlash va kodni ulashmaslik alohida joylashtiriladi.|Yakuniy ko‘rik — Manba yozuvi yaqin masofada, asosiy tavsiyalar auditoriya orqasidan o‘qilishi tekshiriladi.",
        "Reja — Kadrga faqat qurilma va ish uchun zarur asboblar kiritiladi.|Xato — Tasvirda mijoz telefon raqami qolgan qism belgilanadi.|Tuzatish — Kadr qayta olinadi yoki raqam xavfsiz tahrirlanadi. Yakuniy nusxa to‘liq ko‘riladi.",
        "Murojaat — Mijoz aloqa ishlamayotganini bildirdi.|Aniqlashtirish — «Uzilish qachon boshlandi va qaysi qurilmada kuzatildi?» kabi savol beriladi.|Keyingi amal — Tekshiruv rejalashtiriladi va mijozga qachon yangilik berilishi aytiladi.",
        "Topilgan rasm — Asl sahifada rasm muallifi qidiriladi.|Qaror — Litsenziyada taqdimotga qo‘shish va o‘zgartirish shartlari ko‘riladi.|Rasm osti — Muallif, asar nomi, asl havola va litsenziya yoziladi.",
        "Signal — Notanish hisob bir martalik tasdiqlash kodini so‘radi.|Himoya — Kod yuborilmaydi va xabardagi havola orqali kirilmaydi.|Xabar berish — Rasmiy kanal orqali holat aniqlashtiriladi va shubhali hisob haqida tegishli shaxsga aytiladi.",
        "Matn qismi — Maqoladan aynan olingan jumla qo‘shtirnoq bilan ajratiladi.|Rasm qismi — Rasm tagida muallif va litsenziya qisqa ko‘rsatiladi.|Tekshiruv — Manbalar ro‘yxati havolalari asl sahifalarga olib borishi tekshiriladi.",
        "Cheklash — Xabar qayta uzatilmaydi va ruxsat doirasida o‘chiriladi yoki ko‘rinishi cheklanadi.|Bildirish — Mas’ul shaxsga shaxsiy ma’lumotning o‘zini qayta tarqatmasdan vaziyat tushuntiriladi.|Qayd — Vaqt, ta’sir doirasi va ko‘rilgan choralar zarur hajmda hujjatlanadi.",
    ];

    internal static Board? Find(int topic) => topic >= 1 && topic <= Boards.Length ? Boards[topic - 1] : null;
}

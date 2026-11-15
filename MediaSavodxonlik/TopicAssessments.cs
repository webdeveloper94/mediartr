namespace MediaSavodxonlik;

internal static class TopicAssessments
{
    private static readonly string[][] Concepts =
    [
        ["Fakt|Tekshirish mumkin bo‘lgan ma’lumot", "Fikr|Shaxsiy baho yoki munosabat", "Da’vo|Dalil bilan asoslanishi kerak bo‘lgan gap", "Manba|Xabar olingan shaxs, hujjat yoki nashr", "Kontekst|Xabarni tushunishga yordam beradigan vaqt va sharoit"],
        ["Bosma media|Qog‘ozda tarqatiladigan gazeta yoki e’lon", "Radio|Axborotni asosan ovoz orqali uzatadigan kanal", "Raqamli nashr|Materialni internetda e’lon qilib yangilaydigan nashr", "Ijtimoiy media|Foydalanuvchilar post yaratib, ulasha oladigan platforma", "Qayta aloqa|Auditoriyaning izoh yoki murojaat orqali bildirgan javobi"],
        ["Axborot berish|Servis ish vaqti o‘zgarganini e’lon qilish", "O‘rgatish|Printerni sozlash jarayonini ko‘rsatish", "Ishontirish|Mahsulot afzalliklarini ta’kidlab xaridga undash", "Audiovizual shakl|Harakatlanuvchi tasvir va ovoz birikmasi", "Aralash vazifa|Videodars ichida pullik xizmatni ham taklif qilish"],
        ["Aniq alomat|Printer ekranida Paper Jam yozuvi chiqishi", "Qurilma modeli|Bir oiladagi uskunalarni aniq ajratadigan kod", "Rasmiy qo‘llanma|Ishlab chiqaruvchi chiqargan yo‘riqnoma", "So‘rovni toraytirish|Model va xato xabarini qidiruvga birga kiritish", "Manba tanlash|Topilgan sahifaning servis vazifasiga mosligini aniqlash"],
        ["Muallif vakolati|Materialni yozgan shaxsning mavzu bo‘yicha tajribasi", "Yangilik|Manbaning e’lon yoki yangilanish sanasi", "Dalil sifati|Da’voni asoslovchi hujjat yoki takrorlanadigan sinov", "Qurilmaga moslik|Tavsiyaning aynan shu model va tizimga tegishliligi", "Shaffoflik|Manbada bog‘liqliklar va ishlatilgan ma’lumotlar ochiq ko‘rsatilishi"],
        ["Qo‘shtirnoq|So‘zlar ayni tartibda kelgan iborani izlash", "site: filtri|Qidiruvni muayyan sayt yoki domen doirasida cheklash", "Fayl turi|Natijalarni masalan PDF hujjatlar bilan cheklash", "Sana filtri|Belgilangan davr ichidagi natijalarni ajratish", "Qidiruv jurnali|So‘rov, qo‘llangan filtr va topilgan havolani qayd etish"],
        ["Moslik tekshiruvi|Tavsiya qurilma modeli hamda versiyasiga to‘g‘ri kelishini aniqlash", "Sotuvchi tavsifi|Taklifdagi mahsulot kodi va xususiyatlarini ko‘rsatadigan ma’lumot", "Forum tajribasi|Foydalanuvchi kuzatuvi, qo‘shimcha dalil bilan tekshiriladigan maslahat", "Qaror matritsasi|Manbalarni bir xil mezonlar bo‘yicha solishtiruvchi jadval", "Asoslangan tavsiya|Qaror sababini va tayangan manbalarni ko‘rsatadigan xulosa"],
        ["Muallif|Xabarni tayyorlagan yoki e’lon qilgan tomon", "Auditoriya|Xabar mo‘ljallangan odamlar guruhi", "Maqsad|Xabar orqali erishilmoqchi bo‘lgan natija", "Murojaat tili|Atamalar va iboralar orqali kimga yuzlanilganini anglatuvchi belgi", "Da’vat|Auditoriyadan muayyan harakatni bajarishni so‘raydigan ibora"],
        ["Dezinformatsiya|Chalg‘itish maqsadida atay tarqatilgan yolg‘on ma’lumot", "Kontekstdan uzish|Eski suratni yangi voqea dalili sifatida berish", "Shoshiltirish|Tekshirishga vaqt bermay darhol ulashishga undash", "Hissiy ta’sir|Qo‘rquv yoki g‘azab orqali qarorga ta’sir qilish", "Xolis sarlavha|Tekshiriladigan mazmunni bo‘rttirishsiz ifodalash"],
        ["Asl e’lon|Xabar birinchi chop etilgan sahifa yoki hujjat", "Mustaqil dalil|Ayni xabar nusxasiga tayanmagan tasdiq", "Da’voni ajratish|Tekshiriladigan jumlani aniq yozib olish", "Sana tekshiruvi|E’lon va voqea vaqti bir-biriga mosligini aniqlash", "Dalil yetarli emas|Mavjud manbalar tasdiq yoki rad uchun kifoya qilmagan xulosa"],
        ["Taqqoslash sharoiti|Oldin va keyin sinovlarida bir xil vazifa hamda o‘lchov", "Montaj|Kadrlarni kesish va tartiblash orqali tasvirni taqdim etish", "Reklama da’vosi|Mahsulotga oid tekshiriladigan va’da", "Ta’sir vositasi|Musiqa yoki yorqin kadr bilan hissiy munosabat uyg‘otish", "Tekshirish savoli|Da’voning dalilini aniqlashga qaratilgan aniq so‘rov"],
        ["Maqsad|Tomoshabin video yakunida nima qila olishini belgilash", "Kirish|Vazifa va uning ahamiyatini qisqa tanishtirish", "Kadr rejasi|Har bir sahnadagi tasvir, matn va davomiylikni yozish", "Namoyish|Amalning bajarilishini tasvirda ketma-ket ko‘rsatish", "Yakun|Natijani tekshirish yo‘lini eslatish"],
        ["Matnli yo‘riqnoma|Qayta o‘qish uchun qisqa yozma amallar ro‘yxati", "Belgilangan tasvir|Strelka yoki yozuv bilan qurilma portini ajratib ko‘rsatish", "Video|Vaqt davomida bajariladigan harakatni ko‘rsatish", "Subtitr|Eshitiladigan mazmunning ekrandagi yozma ifodasi", "Auditoriyaga mos til|Atama va tafsilotni foydalanuvchi tajribasiga mos tanlash"],
        ["Vizual iyerarxiya|Sarlavha va asosiy xulosani o‘lcham hamda joylashuv bilan ajratish", "Kontrast|Matnning fon bilan yetarli farqi", "Piktogramma|Bitta amal yoki tushunchani ifodalovchi belgi", "Manba yozuvi|Tasvir yoki dalil qayerdan olinganini ko‘rsatuvchi izoh", "O‘qilish sinovi|Tayyor plakatni namoyish masofasidan tekshirish"],
        ["Ovoz sinovi|Yozuvdan oldin shovqin va nutq ravshanligini tekshirish", "Kadr xavfsizligi|Orqa fondagi mijoz ma’lumotlarini suratga olishdan oldin olib tashlash", "Montaj|Keraksiz bo‘laklarni olib, kadrlarni mazmunli tartiblash", "Eksport|Tahrirlangan ishni ijro qilinadigan faylga chiqarish", "Yakuniy ko‘rik|Tayyor faylning tasviri, ovozi va subtitrlarini pleyerda tekshirish"],
        ["Netiket|Onlayn muloqotdagi hurmat va odob qoidalari", "Aniqlashtiruvchi savol|Muammoning tafsilotini ayblovsiz so‘rash", "Maxfiy kanal|Shaxsiy tafsilotni ommaviy chatdan tashqarida muhokama qilish", "Neytral bayon|Shaxsga baho bermay muammoni tasvirlash", "Keyingi qadam|Murojaatga javoban bajariladigan ish va muddatni bildirish"],
        ["Iqtibos|Asl matndan aynan olingan va ajratib ko‘rsatilgan parcha", "Atributsiya|Muallif, asar va manba haqidagi yozuv", "Litsenziya|Materialdan foydalanish va o‘zgartirish shartlari", "Plagiat|Boshqa muallif ishini o‘z nomidan taqdim etish", "Ruxsat so‘rash|Foydalanish sharti noaniq bo‘lsa huquq egasiga murojaat qilish"],
        ["Shaxsiy ma’lumot|Shaxsni aniqlashga imkon beradigan ma’lumot", "Ikki bosqichli tasdiqlash|Kirishda paroldan tashqari alohida dalil talab qilinishi", "Bir martalik kod|Tasdiqlash uchun qisqa muddatga beriladigan maxfiy kod", "Kiberbulling|Raqamli aloqa orqali takroriy tazyiq yoki haqorat", "Maxfiylik sozlamasi|Profil maydonlarini kim ko‘rishini boshqarish"],
        ["Aynan keltirish|Manbadagi jumlani qo‘shtirnoq ichida yozish", "O‘z so‘zi bilan bayon|Manba mazmunini qayta ifodalab baribir havola berish", "Rasm ostidagi yozuv|Tasvir muallifi va foydalanish shartini uning yonida ko‘rsatish", "Manbalar ro‘yxati|Ishda foydalanilgan materiallarni yakunda tartibli qayd etish", "Havola tekshiruvi|Keltirilgan manzil asl materialni ochishini sinash"],
        ["Tarqalishni cheklash|Shaxsiy ma’lumotli xabarni uzatmaslik va ruxsat bilan ko‘rinishini boshqarish", "Xavfsiz eskalatsiya|Holatni mas’ul shaxsga himoyalangan aloqa orqali bildirish", "Minimal ma’lumot|Muammoni hal qilishga zarur tafsilotnigina yuborish", "Hodisani qayd etish|Vaqt, ta’sir doirasi va ko‘rilgan chorani yozib qo‘yish", "Hurmatli javob|Ayblovsiz xabar berish va ko‘rilayotgan chorani tushuntirish"]
    ];

    internal static (string Term, string Definition)[] Read(int topic) => Concepts[topic - 1]
        .Select(value => value.Split('|', 2)).Select(pair => (pair[0], pair[1])).ToArray();

    internal static CourseActivities.Question[] Questions(int topic)
    {
        var concepts = Read(topic);
        var questions = new List<CourseActivities.Question>(10);
        for (int index = 0; index < concepts.Length; index++)
        {
            var (term, definition) = concepts[index];
            var distractors = concepts.Where((_, other) => index != other).ToArray();
            Random.Shared.Shuffle(distractors);
            questions.Add(new(topic, $"«{term}» qaysi izohga mos?", definition,
                distractors[0].Definition, distractors[1].Definition, distractors[2].Definition));
            questions.Add(new(topic, $"«{definition}» qaysi tushunchani ifodalaydi?", term,
                distractors[0].Term, distractors[1].Term, distractors[2].Term));
        }
        return questions.ToArray();
    }
}

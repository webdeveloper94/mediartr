namespace MediaSavodxonlik;

internal static class CourseActivities
{
    internal static readonly (int Topic, string Title, string Steps)[] Independent =
    [
        (1, "Media savodxonligi kundaligi", "Bir kun davomida uchragan uchta xabarni tanlang. Har birining sarlavhasi, e’lon qilingan joyi va sanasini yozing.|Har bir xabarni nima sababdan o‘qiganingizni va u sizga qanday ma’lumot berganini ikki jumlada bayon qiling. Xabar bilan fikrni ajrating.|Kundalikdan eng ishonchli tuyulgan xabarni belgilang. Ishonchingizga ta’sir qilgan ikkita belgini yozing va qaysi ma’lumotni yana tekshirishingizni ayting."),
        (2, "Uch xil media", "Bitta voqeani tanlang. U haqda bosma nashr, elektron OAV va ijtimoiy tarmoqdan bittadan namuna topib, nomi yoki havolasini qayd eting.|Uch ustunli jadvalda format, mo‘ljallangan auditoriya va xabar qanchalik tez tarqalganini solishtiring. Dalil yetishmasa, jadvalda belgilang.|Tez xabardor bo‘lish va batafsil ma’lumot olish uchun qaysi formatni tanlashingizni yozing. Har tanlovga bitta sabab keltiring."),
        (4, "Qidiruv rejasi", "IT servisga oid bitta aniq savol tuzing. Masalan, noutbukda batareya holatini qanday tekshirish mumkinligini so‘rang.|Savolni uch xil ko‘rinishda qidiring. Umumiy ibora, aniq model yoki tizim nomi va qo‘shtirnoqqa olingan kalit iborani sinab ko‘ring.|Har bir so‘rovdan eng mos bitta natijani qayd eting. Qaysi so‘rov savolga eng aniq javob berganini ikki-uch jumlada asoslang."),
        (5, "Manbani baholash jadvali", "Bitta masala bo‘yicha ikki maqola toping. Sarlavha, sayt nomi va havolani yonma-yon yozing.|Jadvalga muallif, sana, keltirilgan dalil va asl manbani kiriting. Biror band ko‘rsatilmagan bo‘lsa, uni alohida belgilang.|Qaysi maqoladan foydalanishingizni tanlang. Tanlovni jadvaldagi ikki dalil bilan izohlang va tekshirilmagan ma’lumotni ko‘rsating."),
        (6, "Qidiruv filtrlari", "Bitta qidiruv iborasini tanlang va filtrsiz chiqqan dastlabki beshta natijani yozib oling.|Aynan shu iboraga avval sana, keyin fayl turi filtrini qo‘llang. Har safar natijalarning yangiligi va vazifaga mosligini solishtiring.|Eng foydali uchta natijani havolalari bilan yozing. Yonida natijani topishda ishlatilgan so‘rov va filtrni ko‘rsating."),
        (7, "Manbalardan tavsiya tayyorlash", "Kompyuterga xizmat ko‘rsatishga oid amaliy savol tanlang va tavsiya kimga mo‘ljallanganini yozing.|Uch manbadan olingan tavsiyalarning umumiy va farqli tomonlarini jadvalda ko‘rsating. Muallif va e’lon sanasini tekshiring.|Ishonchli ikki manbaga tayangan holda uch bandli tavsiya yozing. Har bir band uchun asos bo‘lgan havolani keltiring."),
        (8, "Xabarning auditoriyasi", "Bitta reklama va bitta axborot xabarini tanlang. Qayerda e’lon qilinganini va sarlavhasini yozing.|Har biri uchun muallif, taxminiy auditoriya va maqsadni belgilang. So‘z tanlovi, tasvir yoki murojaat usulidan misol oling.|Tahlilni ikki qisqa xatboshida taqqoslang. Har bir xulosani xabardagi kamida bitta aniq ibora yoki tasvir bilan asoslang."),
        (9, "Manipulyativ belgilar", "Mashq uchun uydirma xabarga ikkita bo‘rttirilgan sarlavha yozing. Ularning o‘quv namunasi ekanini belgilang.|Har bir sarlavhadagi hissiy so‘z, shoshiltirish yoki dalilsiz da’voni ajrating. Bu belgi o‘quvchiga qanday ta’sir qilishini yozing.|Ikkala sarlavhani tekshiriladigan ma’lumotga tayangan betaraf shaklda qayta yozing. Asl va yangi shakllarni yonma-yon taqdim eting."),
        (10, "Manbasiz xabarni tekshirish", "Manbasi ko‘rsatilmagan xabarni tanlang. Xabardan tekshirish mumkin bo‘lgan bitta da’voni ajratib, aynan yozib oling.|Asl e’lonni qidiring, sanani solishtiring va kamida ikkita mustaqil manbadan dalil izlang. Havolalarni tekshirish jadvaliga kiriting.|Da’voni tasdiqlangan, rad etilgan yoki dalili yetarli emas deb baholang. Xulosaga olib kelgan dalillarni va ochiq qolgan savollarni yozing."),
        (12, "Bir daqiqalik ssenariy", "IT mavzusida tomoshabin videodan keyin nima qila olishi kerakligini bitta jumlada belgilang. Auditoriyaning bilim darajasini hisobga oling.|Kirish, asosiy tushuntirish va yakun uchun og‘zaki aytiladigan matnni yozing. Har qismda ekranda nima ko‘rinishini qayd eting.|Umumiy davomiylikni bir daqiqaga moslang. Matnni ovoz chiqarib o‘qib, vaqtni o‘lchang va kerak bo‘lsa qisqartiring."),
        (14, "Bir sahifalik infografika", "Raqamli gigiyenaga oid uchta tavsiya tanlang. Har birini aniq amal shaklida bir jumlada yozing.|Bir sahifada sarlavha, uchta belgi va ularning izohlarini joylashtiring. Har tavsiya bir qarashda ajralib turishini tekshiring.|Manbalarni sahifa ostida ko‘rsating. Infografikani katta ekranga chiqarib yoki uzoqroqdan qarab, shrift va kontrastini tekshiring."),
        (16, "Netiket qoidalari", "O‘quv guruhi chatidagi kelishmovchilikka oid uydirma vaziyat yozing. Tomonlarning fikrini ikki-uch jumlada bayon qiling.|Muammoga tegishli, hurmatli javob yozing. Noaniq joyni aniqlashtirish va kelishuv taklifini kiritishni unutmang.|Javobda qo‘llagan uchta netiket qoidasini belgilang. Har biri vaziyatni qanday yumshatishini qisqacha izohlang."),
        (17, "Manbalarni rasmiylashtirish", "Foydalanish shartlari ochiq ko‘rsatilgan bitta matn va bitta rasm tanlang. Ikkalasining asl sahifasini toping.|Har material uchun muallif, nom, havola va litsenziyani yozing. Litsenziya muallifni ko‘rsatish yoki o‘zgartirishga oid qanday shart qo‘yishini tekshiring.|Matndan qisqa iqtibos keltiring va rasm tagiga mualliflik yozuvini qo‘shing. Yakuniy namunada ikkala manba ham ko‘rinib tursin."),
        (18, "Maxfiylik tekshiruvi", "Uydirma profil yarating va unda ochiq ko‘rinadigan ism, aloqa ma’lumoti, surat va joylashuv kabi maydonlarni sanang.|Har bir maydonni ochiq, cheklangan yoki maxfiy deb belgilang. Qaroringizni zarurat va xavf nuqtayi nazaridan tushuntiring.|Haqiqiy shaxsiy ma’lumot ishlatmasdan profil uchun uchta aniq himoya tavsiyasini yozing. Qaysi sozlama yoki amal har bir tavsiyani bajarishini ko‘rsating.")
    ];

    internal sealed record Question(int Topic, string Prompt, string Correct, string Wrong1, string Wrong2, string Wrong3)
    {
        public string[] Options => [Correct, Wrong1, Wrong2, Wrong3];
    }

    internal static readonly Question[] Questions =
    [
        new(1, "Media savodxonligi nimani anglatadi?", "Media xabarini tushunish va tanqidiy baholash", "Faqat tez matn terish", "Ekran yorqinligini sozlash", "Faqat video tomosha qilish"),
        new(2, "Qaysi biri an’anaviy media?", "Bosma gazeta", "Guruh chati", "Onlayn forum", "Ijtimoiy tarmoq posti"),
        new(3, "O‘quv videoning asosiy vazifasi nima?", "Jarayonni ko‘rsatib tushuntirish", "Qidiruv tarixini saqlash", "Hisob parolini tiklash", "Matnni avtomatik tarjima qilish"),
        new(4, "Qidiruvda iborani qo‘shtirnoqqa olish nimaga yordam beradi?", "Aynan shu iborani topishga", "Barcha rasmlarni yashirishga", "Natijalarni tarjima qilishga", "Tarixni tozalashga"),
        new(5, "Yangilikni baholashda qaysi belgilar muhim?", "Muallif, sana, manba va dalil", "Logotip va fon rangi", "Faqat izohlar soni", "Faqat sarlavha uzunligi"),
        new(6, "Qidiruvdagi sana filtri nima qiladi?", "Tanlangan davrdagi natijalarni ajratadi", "Manbaning rostligini tasdiqlaydi", "Natijalarni alifboga soladi", "Fayllarni yuklab oladi"),
        new(7, "Ikki manba qarama-qarshi yozsa, nima qilish kerak?", "Asl dalillarni va mustaqil manbalarni solishtirish", "Birinchisini qabul qilish", "Uzunrog‘ini tanlash", "Ko‘p ulashilganiga ishonish"),
        new(8, "Xabar auditoriyasi deganda kim nazarda tutiladi?", "Xabar yo‘naltirilgan odamlar", "Faqat xabar muallifi", "Faqat sayt egasi", "Faqat reklama buyurtmachisi"),
        new(9, "Manipulyativ sarlavhaning mumkin bo‘lgan belgisi qaysi?", "Dalilsiz shoshiltirish yoki qo‘rqitish", "Manba havolasi borligi", "Sananing aniq yozilishi", "Masalaning xolis bayoni"),
        new(10, "Eski surat yangi voqea sifatida tarqalsa, nima tekshiriladi?", "Suratning ilk e’loni va sanasi", "Tasvirdagi ranglar soni", "Fayl nomining uzunligi", "Postga qo‘yilgan reaksiyalar"),
        new(11, "Reklamani tanqidiy tahlil qilishda qaysi savol foydali?", "Qanday da’vo qilinyapti va uni nima tasdiqlaydi?", "Shrift qaysi faylda saqlangan?", "Izohlar necha qatordan iborat?", "Rasm ekranning qayerida turibdi?"),
        new(12, "Ssenariy tuzishda avval nima belgilanadi?", "Maqsad va auditoriya", "Yakuniy fayl hajmi", "Eksport papkasi", "Kadrlar orasidagi effekt"),
        new(13, "Katta ekranda slayd o‘qilishini nima yaxshilaydi?", "Yirik matn va aniq kontrast", "Mayda matn va bezakli fon", "Har satrda har xil shrift", "Barcha matnni bitta slaydga joylash"),
        new(14, "Infografikada raqamlar bilan birga nima beriladi?", "O‘lchov birligi va ma’lumot manbasi", "Faqat yorqin ramka", "Faqat muallif avatari", "Tasodifiy belgilar"),
        new(15, "Videoni ulashishdan avval nimani tekshirish foydali?", "Ovoz, tasvir va foydalanish ruxsati", "Faqat fayl nomi", "Faqat montaj dasturi nomi", "Faqat videoning birinchi kadri"),
        new(16, "Qaysi javob netiketga mos?", "Qarshi fikrga hurmat bilan dalil keltirish", "Suhbatdoshni haqorat qilish", "Xabarni ataylab buzib tarqatish", "Birovning yozishmasini ruxsatsiz ulashish"),
        new(17, "Boshqa muallif matnidan iqtibos olganda nima qilinadi?", "Iqtibos belgilanadi va manba ko‘rsatiladi", "Faqat shrift almashtiriladi", "Muallif nomi olib tashlanadi", "Matn o‘z nomidan beriladi"),
        new(18, "Qaysi ma’lumotni ochiq guruhga yuborish xavfli?", "Bir martalik tasdiqlash kodi", "Ochiq dars jadvali havolasi", "Umumiy ob-havo ma’lumoti", "Ochiq lug‘atdagi ta’rif"),
        new(19, "Litsenziya rasm muallifini ko‘rsatishni talab qilsa, nima qilinadi?", "Muallif va litsenziya shartlari ko‘rsatiladi", "Rasmga ramka qo‘yiladi", "Fayl nomi o‘zgartiriladi", "Rasm qisqartiriladi"),
        new(20, "Kiberbullingga duch kelinganda qaysi chora o‘rinli?", "Dalilni saqlash, bloklash va mas’ul shaxsga xabar berish", "Haqorat bilan javob berish", "Xabarni keng tarqatish", "Shaxsiy parolni ulashish")
    ];

    internal static readonly (string First, string Second, string Practice)[] Variants =
    [
        ("Media savodxonligi va axborot savodxonligini ta’riflang.", "An’anaviy va raqamli media farqlarini tushuntiring.", "Gazeta xabari, videodars va ijtimoiy postni turi hamda vazifasi bo‘yicha jadvalga ajrating."),
        ("Axborot manbasining birlamchi va ikkilamchi turlarini tushuntiring.", "Kalit so‘z tanlash qidiruvga qanday ta’sir qiladi?", "Kompyuter xotirasi bo‘yicha uch xil so‘rov tuzing va eng mos natijani asoslang."),
        ("Manba ishonchliligi uchun to‘rtta mezonni ayting.", "Yangilikning asl manbasiga qaytish nimaga xizmat qiladi?", "Bir hodisa haqidagi ikki xabarni muallif, sana, manba va dalil bo‘yicha solishtiring."),
        ("Fakt va fikr o‘rtasidagi farqni tushuntiring.", "Xabar maqsadi va auditoriyasi qanday aniqlanadi?", "Bitta reklama matnidagi ikki fakt da’vosini va ikki fikrni belgilang."),
        ("Soxta xabar va dezinformatsiya tushunchalarini izohlang.", "Hissiy sarlavha auditoriyaga qanday ta’sir qiladi?", "Bo‘rttirilgan e’lonni tanlab, manipulyativ iboralarni belgilang va xolis shaklda qayta yozing."),
        ("Mustaqil dalil nima?", "Eski surat kontekstdan uzib ishlatilganda qanday xato yuz beradi?", "Uydirma e’lon bo‘yicha tekshirish jadvali tuzing. Da’vo, sana, asl manba va ikki tekshirish qadamini kiriting."),
        ("Qidiruv operatorlari qanday yordam beradi?", "Sana va fayl turi filtrlarining vazifasi nima?", "Ikki qidiruv filtrini qo‘llab, boshlang‘ich va yakuniy natijalarni taqqoslang."),
        ("Media mahsulotini yaratish bosqichlarini sanang.", "Ssenariyda vaqtni rejalash qanday yordam beradi?", "Raqamli gigiyena haqida 60 soniyali ssenariy tuzing va har qismga vaqt ajrating."),
        ("Infografikada vizual iyerarxiya nima?", "Matn va fon kontrasti nima uchun muhim?", "Uch qoidadan iborat, manba ko‘rsatilgan bir sahifalik infografika eskizini tuzing."),
        ("Videoxabarda ovoz sifatiga nimalar ta’sir qiladi?", "Video xabarning kirish qismi qanday vazifani bajaradi?", "IT servis bo‘yicha qisqa videoxabar uchun uch kadrli reja va narratsiya matnini yozing."),
        ("Netiket deganda nimani tushunasiz?", "Onlayn muloqotda tanqidni qanday bildirish mumkin?", "Guruh chatidagi nizoga hurmatli javob yozing va ishlatgan ikki qoidani izohlang."),
        ("Mualliflik huquqi va plagiatni tushuntiring.", "Iqtibos uchun qanday ma’lumotlar ko‘rsatiladi?", "Bitta matn parchasi va rasm uchun foydalanish sharti hamda manbasini rasmiylashtiring."),
        ("Shaxsiy ma’lumotga misollar keltiring.", "Raqamli gigiyenaning uchta qoidasini ayting.", "Namunaviy profil ma’lumotlarini ochiq va maxfiy toifalarga ajrating, har qarorni tushuntiring."),
        ("Kiberbulling qanday namoyon bo‘ladi?", "Dalilni saqlash va xabar berish nima uchun muhim?", "Berilgan onlayn haqorat vaziyatida xavfsiz javob berishning to‘rt qadamini yozing."),
        ("Axborotni tekshirishda bir nechta manba qanday yordam beradi?", "Auditoriyaga mos taqdimot qanday tuziladi?", "Bitta texnologik da’voni tekshirish rejasini tuzing va xulosani uch slaydga rejalang.")
    ];
}

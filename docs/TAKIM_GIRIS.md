# 🎮 Escape from Piggy - Takım Giriş Rehberi

Merhaba! Escape from Piggy ekibine hoş geldin! 🐷

Bu doküman, projemize yeni katılan herkese oyunumuz hakkında bilgi vermek ve nasıl katkıda bulunabileceklerini göstermek için hazırlandı. Kafana takılan bir şey olursa ekipten herhangi birine sorabilirsin, hepimiz öğrenerek ilerliyoruz zaten!

---

## 📖 Oyun Nedir?

**Escape from Piggy**, İYTE (IZTECH) kampüsünde geçen bir precision platformer oyunu. Celeste'i oynamışsan ne demek istediğimizi anlarsın - sıkı kontroller, zorlayıcı ama adil platforming, ve "bir kez daha deneyeyim" hissi.

### Kısaca Hikaye
Rektör Yusuf Baran (evet, bizim rektörümüz!) kampüse saldıran yaban domuzlarından kaçarken DNA manipülasyonu güçleri kazanıyor ve 5 paralel evrende maceraya atılıyor. Laser gözler, vampir formu, piggy binme... Evet, absürt ama bu da eğlenceli yanı!

### Neden Bu Oyun?
- İlk kez İYTE ve Türk üniversite kültürünü temsil eden bir oyun yapıyoruz
- Kampüsümüzü, içerideki şakaları, yerlerimizi oyunda göreceğiz
- Ciddi gameplay, komik hikaye - ikisini de seviyoruz
- Celeste seviyesinde zorlukta bir platformer hedefliyoruz

### 5 Paralel Evren
1. **Normal İYTE** - Gerçek kampüsümüzün pixel art hali
2. **Bio-Punk İYTE** - Doğa tarafından geri alınmış, biyolüminesans bitkileri olan kampüs
3. **Retro-Tech İYTE** - 80'ler/90'lar vaporwave estetiği
4. **Cosmic Horror İYTE** - Boşlukta yüzen, eldritch geometrili kampüs
5. **Mirror İYTE** - Tüm gerçekliklerin kesiştiği kristal nokta

---

## 🚀 Projeye Başlangıç

### Gereksinimler
- **Unity Hub** (en son sürüm)
- **Unity 6000.3.3f1 LTS** (en son surum)
- **Git** (version control için)
- **Git LFS** (büyük dosyalar için)
- Visual Studio Code veya tercih ettiğin bir IDE (C# için)

### İlk Kurulum

1. **Repoyu Klonla**
   ```bash
   git clone https://github.com/IYTE-Yazilim-Toplulugu/escape_from_piggy.git
   cd escape_from_piggy
   ```

2. **Unity Hub'da Aç**
   - Unity Hub'ı aç
   - "Add" → Projeyi seç
   - Unity ile aç

3. **İlk Sahneyi Aç**
   - `Assets/_EscapeFromPiggy/Scenes/SampleScene.unity`
   - Play butonuna bas, çalışıyor mu diye kontrol et

4. **Dökümantasyonu Oku**
   - `README.md` - Genel bilgiler
   - `docs/PROJECT_STRUCTURE.md` - Proje yapısı ve kurallar
   - `_bmad-output/game-brief.md` - Oyun tasarım özeti
   - `_bmad-output/gdd.md` - Detaylı tasarım dokümanı

5. **Github Project icinde "Issue"nu bul**
   - Gerekli olan adımları takip et
   - Eğer bir sorun yaşarsan, ekipten yardım iste!

### Git LFS Kurulumu
Eğer daha önce Git LFS kurmadıysan:
```bash
git lfs install
```
Projedeki büyük dosyalar (texture, audio, vb.) otomatik olarak LFS ile yönetiliyor.

---

## 🎨 Ne Tür Görevler Yapabilirsin?

### 1. **Sprite ve Art Tasarımı** 🎨
**Ne yaparsın:**
- Karakter sprite'ları (Player, Piggy, NPC'ler)
- Çevre objeleri (duvarlar, kapılar, mobilyalar)
- UI elemanları (butonlar, menüler, ikonlar)
- Animasyon frame'leri
- Particle effect'ler

**Nereye koyarsın:**
- `Assets/_EscapeFromPiggy/Art/Sprites/` - Sprite'lar
- `Assets/_EscapeFromPiggy/Art/Animations/` - Animasyonlar
- `Assets/_EscapeFromPiggy/Art/UI/` - UI artwork

**Önemli:**
- Cizimleri ve animasyonları katmanlı PSD veya Aseprite dosyası olarak sakla
- Genel olarak Aseprite kullanıyoruz, fakat Adobe Photoshop da kabul edilir
- **Pixel art** kullanıyoruz (16-bit stili)
- Her evrenin kendine özel renk paleti var
- Dosya isimleri PascalCase olmalı: `PlayerIdle.png`, `WoodenDoor.png`
- Unity'de import settings'i Sprite 2D olarak ayarla

### 2. **C# Script Yazma** 💻
**Ne yaparsın:**
- Player hareket ve kontrol sistemleri
- Düşman AI'ları (Piggy davranışları)
- Level mekanikleri (kapı, anahtar, tuzaklar)
- UI kontrolleri
- Game manager'lar
- DNA sistemi implementasyonu

**Nereye koyarsın:**
- `Assets/_EscapeFromPiggy/Scripts/Player/` - Player ile ilgili
- `Assets/_EscapeFromPiggy/Scripts/Enemy/` - AI ve düşmanlar
- `Assets/_EscapeFromPiggy/Scripts/Level/` - Level mekanikleri
- `Assets/_EscapeFromPiggy/Scripts/UI/` - UI kodları
- `Assets/_EscapeFromPiggy/Scripts/Managers/` - Game sistemleri

**Önemli:**
- Tüm kodlar `EscapeFromPiggy` namespace'i içinde olmalı
- Bir dosya = bir class
- camelCase değişkenler, PascalCase classlar
- Private field'lar `_` ile başlamalı: `_rigidbody`

**Örnek:**
```csharp
namespace EscapeFromPiggy
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            // Hareket kodu
        }
    }
}
```

### 3. **Asset Araştırma ve Entegrasyon** 🔍
**Ne yaparsın:**
- Asset Store'dan uygun müzik, SFX, tool'lar bul
- Third-party paketleri entegre et
- Lisansları kontrol et (free/paid, ticari kullanım)
- Test et ve dokümante et

**Nereye koyarsın:**
- `Assets/ThirdParty/` - Dışarıdan gelen her şey burada
- Her asset için bir alt klasör aç
- README ekle (lisans, kaynak, kullanım)

**Önemli:**
- Sadece ticari kullanıma uygun asset'ler
- Lisans bilgilerini sakla
- Unity versiyonuyla uyumlu olup olmadığını kontrol et

### 4. **Level Design** 🗺️
**Ne yaparsın:**
- Unity sahne editöründe level'lar tasarla
- Platform yerleşimleri
- Düşman spawn noktaları
- Gizli alan ve secret room'lar
- Zorluk dengeleme

**Nereye koyarsın:**
- `Assets/_EscapeFromPiggy/Scenes/` - Scene dosyaları
- İsimlendirme: `Level_01.unity`, `TestLevel.unity`

**Önemli:**
- Hierarchy'de organizasyon önemli (aşağıdaki yapıyı kullan)
- Prefab'ları kullan (her objeyi sıfırdan yapma)
- Test et, test et, test et!

### 5. **Prefab Oluşturma** 🧩
**Ne yaparsın:**
- Tekrar kullanılabilir GameObject'ler yarat
- Prefab variant'ları oluştur
- Nested prefab'lar ile kompozisyon

**Nereye koyarsın:**
- `Assets/_EscapeFromPiggy/Prefabs/Characters/` - Karakterler
- `Assets/_EscapeFromPiggy/Prefabs/Environment/` - Çevre objeleri
- `Assets/_EscapeFromPiggy/Prefabs/UI/` - UI prefab'ları

**Önemli:**
- İsimler açıklayıcı olsun: `Player`, `EnemyPiggy`, `WoodenDoor`
- Prefab variant kullan (DoorBase → WoodenDoor, MetalDoor)

### 6. **Testing ve QA** 🐛
**Ne yaparsın:**
- Oyunu oyna ve bug'ları bul
- Zorluk dengesini test et
- Performans kontrolü
- Geri bildirim ver

**Nasıl yaparsın:**
- Play mode'da test et
- Her değişiklikten sonra build al ve test et
- Issue'ları GitHub'a veya Discord'a raporla
- Adım adım nasıl reproduce edileceğini yaz

### 7. **Audio (Müzik/SFX)** 🎵
**Ne yaparsın:**
- Müzik kompozisyonu veya asset bulma
- AI ile muzik sentezleme
- Sound effect'ler (jump, dash, collect vb.)
- Audio mixing ve balans

**Nereye koyarsın:**
- `Assets/_EscapeFromPiggy/Audio/Music/` - Müzikler
- `Assets/_EscapeFromPiggy/Audio/SFX/` - Ses efektleri

**Önemli:**
- `.mp3`, `.wav`, veya `.ogg` formatları
- Dosya boyutlarına dikkat (optimize et)

### 8. **Dokümantasyon** 📝
**Ne yaparsın:**
- Code comment'leri
- Design decision'ları dokümante et
- Tutorial veya guide yaz
- Changelog güncelle

**Nereye koyarsın:**
- Code içinde (XML comments)
- `docs/` klasöründe markdown dosyaları
- Proje wiki

---

## 🤝 Contribution (Katkı) Kuralları

### Git Workflow

1. **Branch Oluştur**
   ```bash
   # Main/dev branch'inden yeni branch oluştur
   git checkout dev
   git pull origin dev
   git checkout -b feature/senin-feature-ismin
   ```

2. **Değişiklikleri Yap**
   - Küçük, mantıklı commit'ler at
   - Her commit bir şeyi tamamlamalı
   - Commit mesajları açıklayıcı olsun

3. **Commit At**
   ```bash
   git add .
   git commit -m "feat: player double jump eklendi"
   ```

4. **Push Et ve Pull Request Aç**
   ```bash
   git push origin feature/senin-feature-ismin
   ```
   - GitHub'da Pull Request aç
   - Açıklama yaz, ne yaptığını anlat
   - Review bekle

### Commit Mesaj Formatı

Şu formatı kullan:
```
<tip>: <kısa açıklama>

<uzun açıklama (opsiyonel)>
```

**Tipler:**
- `feat`: Yeni özellik
- `fix`: Bug düzeltmesi
- `docs`: Dokümantasyon
- `style`: Code formatı (logic değişmedi)
- `refactor`: Code refactor (davranış aynı)
- `test`: Test ekleme/düzeltme
- `chore`: Build, konfigürasyon, vb.

**Örnekler:**
```
feat: laser eyes ability eklendi
fix: player double jump bug'ı düzeltildi
docs: PROJECT_STRUCTURE.md güncellendi
refactor: PlayerController cleanup
```

### Unity Özel Kurallar

#### ⚠️ .meta Dosyaları
**ÇOK ÖNEMLİ:** Unity'deki her asset için `.meta` dosyası oluşur (bunlar otomatik olusur). Bu dosyalar olmadan Unity referansları kaybeder, `.gitignore'a ekleme`!

#### Scene Değişiklikleri
- Scene'leri aynı anda birden fazla kişi düzenlemesin (conflict olur)
- Büyük değişiklikler yaparken ekibe haber ver
- Test scene'lerini `_Dev/SenınAdın/` altında yap

#### Prefab Workflow
- Prefab'larda değişiklik yaparken dikkatli ol (her yere etkiler)
- Override'ları minimize et
- Prefab variant kullan (base prefab'ı değiştirmek yerine)

### Code Style

#### Naming Conventions
- **Classes:** PascalCase → `PlayerController`, `EnemyAI`
- **Variables:** camelCase → `playerSpeed`, `maxHealth`
- **Private fields:** `_camelCase` → `_rigidbody`, `_isGrounded`
- **Constants:** UPPER_SNAKE_CASE → `MAX_HEALTH`, `DEFAULT_SPEED`
- **Methods:** PascalCase → `HandleMovement()`, `TakeDamage()`

#### File Organization
```
Assets/
├── _EscapeFromPiggy/          # Ana oyun
│   ├── Art/                   # Görseller
│   ├── Audio/                 # Sesler
│   ├── Prefabs/               # Prefab'lar
│   ├── Scenes/                # Sahneler
│   ├── Scripts/               # C# kodları
│   ├── ScriptableObjects/     # Data asset'leri
│   └── Settings/              # Unity ayarları
├── ThirdParty/                # Dışarıdan paketler
└── _Dev/                      # Developer sandbox (bireysel testler)
```

#### Scene Hierarchy Organization
```
SampleScene
├── --- MANAGEMENT ---
│   ├── GameManager
│   ├── AudioManager
│   └── EventSystem
├── --- ENVIRONMENT ---
│   ├── Ground
│   ├── Walls
│   └── Props
├── --- GAMEPLAY ---
│   ├── Player
│   ├── Enemies
│   └── Collectibles
├── --- UI ---
│   └── Canvas
└── --- LIGHTING ---
    ├── Global Volume
    └── Lights
```

Boş GameObject'lerle `---` prefix kullanarak ayır.

---

## 📚 Kaynaklar ve Öğrenme

### Proje Dökümantasyonu
- `README.md` - Proje genel bakış
- `docs/PROJECT_STRUCTURE.md` - Detaylı yapı ve kurallar
- `_bmad-output/game-brief.md` - Oyun tasarım özeti
- `_bmad-output/gdd.md` - Game Design Document

### Unity Öğrenme
- [Unity Learn](https://learn.unity.com/) - Resmi Unity kursları
- [Brackeys YouTube](https://www.youtube.com/user/Brackeys) - Unity tutorial'ları (Türkçe alt yazılı)
- [Celeste Movement Tutorial](https://www.youtube.com/watch?v=STyY26a_dPY) - Precision platformer kontrolü

### Git Öğrenme
- [GitHub Git Handbook](https://guides.github.com/introduction/git-handbook/)
- [Türkçe Git 101](https://www.youtube.com/watch?v=rWG70T7fePg)

### C# ve Unity Best Practices
- [Unity C# Style Guide](https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity)
- [Microsoft C# Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

### Pixel Art
- [Pixel Art Tutorials](https://lospec.com/pixel-art-tutorials)
- [Aseprite](https://www.aseprite.org/) - Pixel art editor (ücretli ama güzel)

---

## 💬 İletişim ve Yardım

### Soru Sormaktan Çekinme!
Kimse her şeyi bilmiyor, hepimiz öğreniyoruz. Takıldığın yerde:
- GitHub issue aç
- Toplantılarda konuş
- Ekip liderine ulaş
- Whatsapp'tan yaz

### Code Review
- Pull request açtığında feedback bekle
- Feedback'leri kişisel algılama, hepimiz gelişiyoruz
- Başkalarının PR'larına da bak, öğrenme fırsatı

### Pair Programming
İki kişi beraber kod yazmak çok faydalı:
- Birisi yazar, diğeri düşünür
- Zorlukları beraber çöz
- Ekip olarak öğren

---

## 🎯 İlk Görevin

Projeye yeni katıldıysan, işte başlangıç için öneriler:

1. **Kurulumu Tamamla**
   - Unity projesini aç ve çalıştır
   - Git'i kur ve repoyu klonla

2. **Dökümantasyonu Oku**
   - README.md
   - PROJECT_STRUCTURE.md
   - Game Brief ve GDD'ye göz at

3. **Oyunu Oyna**
   - Varsa mevcut build'i dene
   - Ne yaptığımızı anla

4. **Basit Bir Görev Seç**
   - "Good first issue" etiketli GitHub issue'larına bak
   - Veya ekip liderinden küçük bir görev iste
   - İlk PR'ını aç!

---

## 🐷 Final Söz

Escape from Piggy sadece bir oyun değil, İYTE topluluğumuzun hikayesi. Her bir sprite, her bir kod satırı, her bir level tasarımı bu hikayeye katkı sağlıyor.

Hata yapmaktan korkma. İlk commit'in perfect olmayacak, ilk sprite'ın placeholder kalabilir, ilk kodun refactor edilebilir - hepsi normal! Önemli olan denemek ve öğrenmek.

Hoş geldin, hadi beraber harika bir şey yapalım! 🚀

**Oyun Akışı ve Durum Yönetimi**

1. **GameInstaller Sınıfı**:
   - `GameInstaller` sınıfı, oyunun ana yapılandırıcısıdır. Bu sınıf, oyunun başlangıcında ve sonunda gerekli ayarlamaları yapar.
   - `Awake()` yöntemi, oyunun başlatılmasından önce gerekli bileşenleri oluşturur ve hazırlar.
   - `Start()` yöntemi, oyun başladığında `AppState`'in etkinleştirilmesini sağlar.
   - `Update()` yöntemi, her güncelleme döngüsünde `AppState`'in güncellenmesini sağlar.
   - `OnApplicationQuit()` yöntemi, uygulama kapatıldığında temizlik işlemlerini gerçekleştirir.

2. **AppState Sınıfı**:
   - `AppState`, oyunun ana durum makinesidir. Oyunun farklı durumlarını (loading, lobby, game) yönetir.
   - `AppState`, `LoadingState`, `LobbyState`, ve `GameState` olmak üzere üç alt durumu yönetir.
   - `LoadingState`, oyun yüklenirken bir yükleme ekranı gösterir.
   - `LobbyState`, oyuncunun lobi arayüzünde dolaşmasını sağlar ve seviye başlatma gibi eylemleri gerçekleştirir.
   - `GameState`, oyunun ana oyun akışını yönetir ve `PrepareGameState`, `InGameState`, `LevelCompleteState`, ve `LevelFailState` alt durumlarını yönetir.

3. **InGameState Sınıfı**:
   - `InGameState`, oyunun ana oyun durumunu yönetir. Oyuncunun oyun alanında etkileşimde bulunmasını sağlar.
   - `InGameState`, `IdleState`, `OnMouseDownState`, ve `OnMouseUpState` alt durumlarını yönetir.
   - Fare etkileşimlerini işleyen alt durumlar, oyuncunun fare tıklamalarına göre koleksiyonları işler.

4. **LevelCompleteState ve LevelFailState Sınıfları**:
   - Bu sınıflar, oyunun tamamlanma veya başarısızlık durumlarını yönetir.
   - `LevelCompleteState`, seviye tamamlandığında oyuncuya geri bildirim sağlar ve bir sonraki adıma geçmesi için seçenekler sunar.
   - `LevelFailState`, seviye başarısız olduğunda oyuncuya geri bildirim sağlar ve tekrar denemesi veya lobiye dönmesi için seçenekler sunar.

Bu adımlar, oyunun ana akışını ve durumlar arasındaki geçişleri özetlemektedir. Oyunun daha spesifik işlevleri veya durumları varsa, bu yapıya daha fazla alt durum ve işlev eklenebilir. Ancak, yukarıda belirtilen yapı, genel bir oyun akışını anlamak için yeterli bir çerçeve sağlar.

**ComponentContainer Sınıfı ve Bağımlılık Yönetimi**

1. **ComponentContainer Sınıfı**:
   - `ComponentContainer`, oyun içinde kullanılan bileşenlerin yönetimini sağlayan bir sınıftır.
   - Bu sınıf, oyun içindeki farklı bileşenlerin eklenmesini, çözümlenmesini ve yönetilmesini sağlar.
   - `AddComponent()` yöntemi, belirli bir bileşeni ekler ve ilgili arayüzleri uygular. Bu bileşenler genellikle `IInitializable`, `IStartable`, `IUpdatable`, ve `IDisposable` arayüzlerini uygular.
   - `Resolve()` yöntemi, henüz eklenmemiş bir bileşeni ekler.
   - `GetComponent()` yöntemi, belirli bir türdeki bileşeni alır.
   - `InitializeComponents()`, `StartComponents()`, `Update()`, ve `Dispose()` yöntemleri, sırasıyla bileşenlerin başlatılmasını, güncellenmesini ve temizlenmesini sağlar.

2. **Bağımlılık Yönetimi**:
   - `ComponentContainer`, bağımlılıkların yönetilmesini sağlar. Bu, farklı bileşenlerin birbirine bağlı olduğu durumları ele alır.
   - Her bileşenin, ilgili arayüzleri uygulayarak, hangi durumlarda başlatılacağı, güncelleneceği veya temizleneceği belirtilir.
   - Bu yapı, bileşenler arasındaki bağımlılıkları yönetir ve sınıflar arasındaki etkileşimi kolaylaştırır.
   - Bileşenlerin otomatik olarak başlatılması, güncellenmesi ve temizlenmesi gibi işlevler, bu yapı sayesinde merkezi olarak yönetilir.

Bu sınıf, oyun içindeki bileşenler arasındaki bağımlılıkları yönetmek ve bileşenlerin doğru bir şekilde başlatılmasını, güncellenmesini ve temizlenmesini sağlamak için kullanılır. Bu, oyunun genel performansını artırır ve kod tekrarını azaltır.

**StateMachine Sınıfı ve Durum Yönetimi**

1. **StateMachine Sınıfı**:
   - `StateMachine`, oyun içindeki durumları yöneten soyut bir sınıftır.
   - Bu sınıf, durumlar arasındaki geçişleri kontrol eder ve her durumun giriş, güncelleme ve çıkış işlemlerini yönetir.
   - `Enter()`, `Update()`, ve `Exit()` yöntemleri, sırasıyla bir durumun başlatılmasını, güncellenmesini ve sonlandırılmasını sağlar.
   - `AddTransition()`, belirli bir durumda bir tetikleyiciye bağlı olarak başka bir duruma geçişi ekler.
   - `AddSubState()`, alt durumları ekler ve varsayılan bir alt durum belirler.
   - `SendTrigger()`, belirli bir tetikleyiciyi kullanarak bir durum değişikliği talebi gönderir.

2. **Durum Yönetimi**:
   - `StateMachine`, durumların hiyerarşik bir yapısını sağlar. Her durum altında bir veya daha fazla alt durum bulunabilir.
   - Durumlar arasındaki geçişler, tetikleyiciler ve hedef durumlar arasında tanımlanır.
   - Her durum, başlatma, güncelleme ve sonlandırma işlemlerini yönetir. Bu işlemler, oyun durumunu yönetmek için temel işlevlerdir.
   - Durumlar, soyut `OnEnter()`, `OnUpdate()`, ve `OnExit()` yöntemlerini uygulayarak özelleştirilebilir.
   - Durumlar arasındaki geçişler, durumlar arasındaki ilişkileri ve oyunun akışını kontrol eder. Bu, oyunun farklı bölümleri arasındaki geçişleri yönetmek için önemlidir.

Bu sınıf, oyun içindeki farklı durumları (örneğin, loading, lobby, game) yönetmek için kullanılır. Her durum, oyunun belirli bir bölümünü temsil eder ve bu bölümdeki işlevleri ve geçişleri kontrol eder. Bu, oyunun genel akışını organize etmek için kullanılan güçlü bir araçtır.

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

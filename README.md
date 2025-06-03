# 🧠 Domain Driven Design (DDD) Rehberi

### 📚 DDD Tanımı

**Domain Driven Design**, karmaşık yazılım projelerinde iş gereksinimlerini teknik tasarıma doğru şekilde yansıtmak amacıyla kullanılan bir yazılım geliştirme yaklaşımıdır.
DDD, iş alanını (**domain**) derinlemesine anlamaya ve bu alanı yazılımın merkezine koymaya odaklanır.
Bu yaklaşımda yazılım bileşenleri, iş kurallarına göre modellenir ve teknik detaylardan bağımsız olarak, domain uzmanlarıyla geliştiriciler arasında ortak bir dil (**ubiquitous language**) oluşturularak geliştirme süreci kolaylaştırılır.
🎯 **Amaç**: İş ihtiyaçlarını daha doğru karşılayan, esnek ve sürdürülebilir sistemler geliştirmektir.

---

### 🧩 DDD Temel Kavramlar

#### 🗣️ Ubiquitous Language

Geliştiriciler ve domain uzmanları arasında kullanılan ortak, tutarlı ve anlamlı bir dildir. Bu dil, hem iş dünyasının hem de yazılım sisteminin kavramlarını tanımlar ve modelin kendisini ifade eder.
Bu dilin amacı:

- 🤝 Anlaşmazlıkları azaltmak
- 💬 İletişimi kolaylaştırmak
- 🔄 Kodu ve modeli senkronize tutmak

📌 **Örnek Senaryo:**
Bir ödeme sistemi geliştiriyorsak:

- Domain uzmanı "Ödeme talebi onaylandıktan sonra işlem başlatılır" diyorsa,
- Kodda `PaymentRequest`, `ApprovalStatus`, `InitiateTransaction()` gibi terimler kullanılmalıdır.

Böylece iş dünyası ile yazılım dünyası arasında anlamsal bir köprü kurulur.

---

#### 📦 Bounded Context

Bir modelin net sınırlarla tanımlandığı ve Ubiquitous Language'in tutarlı biçimde kullanıldığı belirli bir bağlamdır.
Başka bir deyişle, kavramların ve kuralların tek anlamlı olduğu, net bir sınırla ayrılmış bir alandır.

🔁 Aynı terim farklı bağlamlarda farklı anlamlara gelebilir — bu da yazılım projelerinde ciddi karışıklıklara yol açabilir.

📌 **Örnek:**
"Kart" kelimesi:

- 🛒 Markette: kredi kartı
- 🎮 Oyunda: oyun kartı
- 🏦 Bankada: banka kartı

Her biri farklı bağlamda (**context**) geçer. Bu yüzden her bir bağlamı ayırmak gerekir. Bu sınırlarla ayrılmış alanlara **Bounded Context** denir.

📌 **Başka bir örnek:**
Gerçek dünyada "order" terimi:

- 🧾 Sipariş yönetim sisteminde: sepete eklenen ama ödemesi yapılmamış sipariş
- 💳 Ödeme sisteminde: ödeme sonrası oluşan sipariş

---

#### 🧱 Entity

Kimliği (ID'si) olan ve zamanla durumu değişse bile kimliği sabit kalan varlıklardır.
Bir varlık, özelliklerinden değil **kimliğinden** tanınır.

---

#### 🎯 Value Object

Kimliği olmayan, sadece sahip olduğu değerler ile tanımlanan nesnelerdir.
Eğer iki value object'in tüm özellikleri aynıysa onlar **birbirinin aynısıdır**.

🔔 **Not:** Value object'teki değerler entity içerisine property olarak doğrudan geçerse veri ile ilgili sorun oluşmaz. Ancak burada amaç, kendine özel iş kuralları (business logic) içerebilecek yapıları alt sınıflara ayırarak daha anlaşılır bir kod yapısı oluşturmaktır.

📌 **Örnekler:**

- 🏠 Address
- 💰 Money

---

#### 🧩 Aggregate

Bir grup **Entity** ve **Value Object’in**, birlikte çalışarak bir iş kuralını temsil ettiği mantıksal birimlerdir.

---

#### 🔐 Aggregate Root

Aggregate’in dış dünyaya açılan tek kapısıdır.

📌 **Örnek:**
`Order` sınıfı **aggregate root**, içerisindeki `orderItems` ise entity'dir.
`orderItems` tek başına anlam ifade etmez, bu yüzden erişimleri sadece `Order` sınıfı üzerinden methodlar ile gerçekleşir.
🔒 **Encapsulation** uygulanır.

---

#### 🛠️ Services

İş kuralları ve doğrulama işlemlerinin uygulandığı yapılardır.

---

#### 🗄️ Repository

Veritabanı iletişimini gerçekleştirmek için kullanılır.

---

#### 🏗️ Layered Architecture

Temelde 4 katmanlı bir mimariden oluşur:

- 🧠 **Domain Layer** – İş kuralları ve modellerin bulunduğu katman
- ⚙️ **Application Layer** – Uygulama iş akışlarının yönetildiği katman
- 🔌 **Infrastructure Layer** – Veritabanı, dış servisler gibi teknik detayların yer aldığı katman
- 🎨 **Presentation Layer** – Kullanıcı arayüzlerinin yer aldığı katman

---

### 🌟 Örnek Proje: **OrderService**

#### 📁 `Order.Domain`

**SeedWork** klasörü içerisine temel sınıflar eklendi:

- 🧱 **BaseEntity**: Id’si olan, veritabanına kaydedilecek tüm entity’lerin temel sınıfı.
- 🧹 **IAggregateRoot**: Bounded Context içinde dış erişime açık kök entity’leri işaretler.
- 📦 **IRepository**: Veri erişim katmanında kullanılacak temel repository arayüzü.
- 🔄 **IUnitOfWork**: Transaction yönetimi için kullanılan birimler.
- 🧬 **ValueObject**: Kimliği olmayan, değerlerine göre eşitlik kontrolü yapılan objeler.

**AggregateModels** içerisinde `OrderModels` klasörü eklendi:

- 🏠 **Address**: Bir Value Object'tir. Kimliği yoktur, veritabanında ayrı tablosu bulunmaz.
- ✅ **Order**: AggregateRoot’tur. Dışarıdan erişim sağlanabilen ana sınıftır.
- 📄 **OrderItem**: Bir Entity’dir. Ayrı tablosu bulunur fakat doğrudan erişim sağlanmaz.

**Events** klasörü içerisinde `OrderStartedDomainEvent` sınıfı eklendi:

- ✨ **OrderStartedDomainEvent**: MediatR kütüphanesindeki `INotification` arayüzü ile şekillendirilmiş, mesaj taşıyan domain eventi.

---

#### 📂 `Order.Application`

**DomainEventHandler** klasörü içerisinde event handler sınıfları yer almakta:

- 🔧 **OrderStartedDomainHandler**: `INotificationHandler` arayüzü implemente edilir. `Handle` metodu içerisinde iş kuralları çalıştırılır, gerekirse diğer bounded context'lerle iletişime geçilir.

**Repository** klasörü içinde base entity’lere özel imzalar tanımlanır:

- 🔍 **IOrderRepository**: `IRepository` arayüzü temel alınarak, `Order` entity’sine özel metotlar eklenir.

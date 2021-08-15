# MyArticle
Basic bir makle uygulamasıdır.

## Proje Kurulumu
- Docker kullanarak projeyi başlatmak için docker-compose dosyasının olduğu dizinde aşağıda ki komutu çalıştırın.
```
docker-compose up
```

Eğer docker kullanılmayacaksa, postgresql kurulumu yapılmalı ve connection string düzenlenmeli
[Postgresql için örnek bir kurulum yazısı](https://tufandayi.wordpress.com/2018/10/01/postgresql-kurulumu/)

Kurulum yapıldıktan sonra user, parola ve host bilgileri düzenlenmelidir.
Muhtemel bilgiler; host:localhost, user:postgres, password: kurulumda girilen değer

Postgresql kurulumu tamamlandıktan sonra postgre üzerinde "myarticle" isimli bir database yaratmak gerekli
[DB oluşturma için örnek bir yazı](https://www.guru99.com/postgresql-create-database.html)

## Migrations
**Kurulum bittikten sonra uygulamayı ilk çalıştırmada entity yapılarını database üzerinde uygulamak gereklidir.**
Migrationların database'e uygulanması için "src\Services\Article\Article.Infrastructure" dizininde aşağıda ki komut çalıştırılır
```
dotnet ef database update
```

Projenin varsayılan linki: http://localhost:5000


Methodlar hakkında bilgi almak için aşağıda ki dokümantasyon inceleyebilirsiniz.
[Postman Dokümantasyon](https://documenter.getpostman.com/view/8176140/T1DjkfJk)

## Design Patterns
### Repository Pattern
Veritabanı üzerinde yapılacak işlemlerin tek noktadan yapılması ile iş katmanına bu yapıların geçmesine engel olunur ve kod tekrarının önüne geçilmesi sağlanır.
En çok tekrarlaması muhtemelen olan kodlar veritabanı işlemleri olduğu için merkezi yönetim ile daha temiz bir yapı oluşturulur.
Test yaparken mock yapılmak istendiğinde gayet basit bir şekilde testleri tamamlama imkanı sunar.

## Unit Of Work
Bir iş transactionı başladığında değişen tüm objelerin listesini tutarak, transaction yönetimini tek bir noktadan yapar. Bunun bize faydası, yapılan değişikliklerin veritabanına kalıcı olarak yazılması aşamasın da eş zamanlılık problemini çözer. Yani iş süreci tamamlandığında eğer herşey doğruysa tüm objelerin aynı anda veritabanına yazılmasını mümkün kılar.

## Facade Design Pattern
Süreç içerisinde ki yapıların, istemci tarafından soyutlanarak kolay kullanım sağlayan tasarımdır.
Karmaşık ve detaylı bir sistemi iş mantıklarına göre alt sistemler olarak ayırabiliyorsak ve kolay kullanım bir alt sistem yazmak istediğimizde bu tasarımı kullanmalıyız.
Facade classları birbirinden bağımsız olmalıdır. Her class kendi sorumluluğuna sahiptir. Böylece karmaşık bir yapı yerine sadece ve kolay anlaşılır bir sistem oluşturmuş oluruz. Loosely-coupled uygulama çıkarmamızı sağlar.

## Decorator Design Pattern
Runtime zamanında nesnenin özellikleri değişiyorsa bu pattern kullanılmalıdır. Bu pattern bir nesneye runtime zamanında kalıtım kullanmadan yeni bir özellik eklemek için kullanılır. Loosely-coupled uygulama çıkarmamızı sağlar. Bu tasarımın kullanıldığı yerlerde open-closed prensibinin sağlandığını açıkça görebiliriz.


## Kullanılan Teknolojiler
- Entity Framework:
Daha önce bireysel projemde kullandığım bir teknoloji. Veritabanı işlemlerini kolayca çözebildiğim bir platform.
- Linq:
Daha önce hem bireysel çalışmalarımda hemde iş hayatımda kullandığım. Objeler üzerinde çalışırken büyük kolaylık sağlayan query kütüphanesi.
- Xunit:
Daha önce bireysel çalışmalarımda kullandığım bir kütüphane. Unit test için kullandığım bir kütüphane. Gayet kolay kullanımı var ve hazırladığım datalar ile birden çok case test edebiliyorum.
- AutoMapper:
Bireysel çalışmalarımda kullandığım kütüphane. Nesnelerin dönüşümlerinde kullandığım bir kütüphanedir. Basit bir kaç tanım ile 2 nesnesi dönüştürebilirsiniz.
- PostgreSql:
Yine bireysel çalışmalarımda kullandığım bir teknoloji. Sorunsuz ve kolay bir sistem.
- Docker
İş hayatımda ve tüm bireysel çalışmalarımda kullandığım bir teknoloji. Her aşamada çok kolaylık sağlayan bir sistem. Çok hızlı bir şekilde uygulamayı ayağa kaldırabildiğim yada deploy edebildiğim nimet.
- Bogus: 
Unittestler için kurallara göre dinamik veri oluşturan yardımcı kütüphane

## ilerde Tamamlamak İstediğim İşler
- [ ] Microservis mimarisi kurmak. Makalelerin görsellerini yükleyeceği bir uygulama ve yazar bilgilerini tutan bir uygulama yapmak.
- [ ] Swagger kurulumu. Servisler testleri için kolay bir arayüz
- [ ] ELK stack. sistemin loglarını elasticsearch üzerinde tutmak ve kibana ile monitör etmek.
- [ ] IdentityServer kurulumu.

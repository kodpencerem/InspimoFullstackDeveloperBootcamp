# Middlewares
Uygulamada her request'de yazýldýðý sýrayla çalýþýp yapýlan isteði yani context nesnemizi alarak gerekli kontrol ya da iþlemi yaptýktan sonra sýradaki middleware'e aktaran yapýlardýr.
Kullanmak için IMiddleware interface'ini bir class'a implement edebiliriz, ya da app.Use(async (context,next) => {await next(context)}) ile kullanabiliriz.

Eðer IMiddleware yöntemini kullanýrsak mutlaka DI yapmamýz ve middleware'i app.useMiddleware ile çaðýrmamýz gerekir.

Genel kontrol için kullanýlabilir
Ýlk userý oluþturma
Database migration otomatikleþtirme
Exception handler => hata iþleme
vb iþlemler için kullanýlabilir



# Filters
Filter çeþidine göre metodun belirli bir parçasýnda araya girip istenilen bir iþlemi gerçekleþtirir. Kullanmak için bireysel olarak metot bazlý çaðrýlmalýdýr. Genelde Attribute'e dönüþtürülüp kullanýlýr.
Örneðin : 
Metot çalýþmadan önce log kaydý yapmak istiyorsak kullanabilir
Metot sonunda Cacheleme yapmak istiyorsak kulanýlabilir
Metot çalýþmadan önce Authentication, Authorization iþlemleri yapmak istiyorsak kullanabilir

## Filter Listesi
### IActionFilter
Ýki tane metoda sahip, Executing ve Executed.
Bu metotlar eklendiði metodun baþýnda veya sonunda çalýþýr, Context nesnemizi Body okuyabilecek þekilde bize verir ve normalde Body kýsmý kitli gelirken Filter sayesinde açýk þekilde elde edebiliriz.
Genelde Cachele, Loglama iþlemleri için kullanýlýr



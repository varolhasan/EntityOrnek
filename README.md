# EntityOrnek
windows forms application (.Net Framework) ile entity framework ve Linq sorgularını kullandığım bir uygulama yaptım. DbFirst yaklaşımı kullandım. Contains, join ve birkaç metodu kullandım. Projede Öğrenci not sistemi oluşturduk ve ilişkili tablolar kullandık. Checkboxlarla id'ye göre sıralama gibi  14 farklı listeleme yaptım. 

Entity Framework normal ADO.Net sorgularına göre çok daha kısa kodlar yazmamızı sağlar.
ADO.NET kullanarak birkaç button kodunu bu şekilde uzun bir kodlamayla yazdım.
Çoğu kısmı ise Entity Framework kullanarak yazdım.

Projede design kısmı aşağıdaki gibidir.

![Ekran Görüntüsü (1121)](https://user-images.githubusercontent.com/100023946/189552801-c4a2150a-7d2e-48d6-96a8-f1145ffe4825.png)

Checkbox'lardan birini seçip Linq Entity dediğimizde, checkbox'a göre listeleme yapacaktır. 

Öğrenci Listele, Ders Listesi, Not Listesi DbFirst ile SQL tarafında oluşturduğumuz tablolar ile listeleme kodlarını yazdım. Join ile Getir butonu ise 3 tablonun birleşimidir. Buradaki amaç ilişkili tablolarda ders id'si ve öğrenci id'si yerine diğer tablodan isimlerini çekmektir. Basit bir MS SQL inner joine benzer bir işlemdir.

Güncelle, Kaydet, Sil butonları dersler ve öğrenci groupboxlarındaki id ve ad textboxlarına veri girişine göre koşullu işlem yapacak şekilde kodlarını yazdım. 

Prosedür kullanımını MS SQL Server kısmında bir procedure oluşturarak VS'da kullandım. Projeye procedure dahil etmeyi (update model from db procedure)

![Ekran Görüntüsü (1287)](https://user-images.githubusercontent.com/100023946/190030352-20b43124-7761-4b8b-9902-64d1382bb16e.png)


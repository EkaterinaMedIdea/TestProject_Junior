1. Первый баг (в карточке пациента добавить дату рождения в будущем) был обнаружен в классе App.xaml.cs, в методе Page_Loaded. 
Он заключался в том, что в элементе DatePicker свойства DisplayDateStart и DisplayDateEnd позволяют отобразить диапазон дат.
Для того, чтобы добавить дату рождения в будущем, можно убрать или закомментировать свойство DisplayDateEnd.

![1](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/db252fc9-9824-4c46-a571-3ace6bf48cb9)


2. Второй баг: System.Data.Entity.Infrastructure.DbUpdateException: 'An error occurred while updating the entries. See the inner exception for details.' 
Inner Exception :
SqlException: The conversion of a datetime2 data type to a datetime data type resulted in an out-of-range value. The statement has been terminated.
(Преобразование типа данных datetime2 в тип данных datetime привело к получению значения, выходящего за пределы диапазона. Действие заявления было прекращено)

 ![2](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/143a4048-f0a8-4c84-958c-9dc2a7783314)

Баг был обнаружен в классе ClinicDataRepository в методе AddPatientCard. Ошибка возникла из-за того, что нельзя добавить дату ранее чем 01.01.1753,
так как в MS SQL при создании таблицы PatientCards в БД в столбце DateOfBirth по умолчанию создается тип данных datetime.

 ![3](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/9a365dbc-d5c9-46b0-9b5d-78a341604a06)
 
Чтобы создавался тип данных datetime2, можно добавить атрибут к свойству DateOfBirth:

 ![4](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/519fc7c8-0b6e-4ff3-b890-46e7636ea43c)
 
Или воспользоваться Fluent API, добавив метод OnModelCreating в класс контекста данных:

![12](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/304a8e86-3b59-4776-8b38-670bb47fdb6e)

После этого нужно обновить существующую БД (update-database).
(P.S: При вводе команд в консоли менеджера пакетов (add-migration, update-database) у меня возникали ошибки «Value cannot be null. Parameter name: type».
И для решения этой проблемы я обновил пакет Entity Framework с 6.2 до 6.4.4. Перезапустил Visual Studio и команды заработали.)


3. Третий баг (при редактировании существующего обращения пациента, приложение падает): System.Data.Entity.Infrastructure.DbUpdateConcurrencyException:
'Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded.
See http://go.microsoft.com/fwlink/?LinkId=472540 for information on understanding and handling optimistic concurrency exceptions.'
Inner Exception
OptimisticConcurrencyException: Store update, insert, or delete statement affected an unexpected number of rows (0). Entities may have been modified or deleted since entities were loaded.
See http://go.microsoft.com/fwlink/?LinkId=472540 for information on understanding and handling optimistic concurrency exceptions.
(Исключение OptimisticConcurrencyException: инструкции Store update, insert или delete повлияли на неожиданное количество строк (0).
Объекты, возможно, были изменены или удалены с момента загрузки объектов. Видишь http://go.microsoft.com/fwlink/?LinkId=472540 для получения информации о понимании
и обработке оптимистичных исключений параллелизма.)

 ![5](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/3a22cc11-0077-432a-aa3f-535626bda75b)

Здесь ошибка в том, что не нужно создавать нового пациента, а только получить его данные, которые уже есть.
Для этого в классе AddModifyRequest.xaml.cs во время редактирования обращения можно закомментировать или убрать строку, где свойству Patient присваивается новый объект.

 ![6](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/0cc8f1c6-b3fb-4439-9da8-b97139b0cbdf)
 
До:

 ![7](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/771e3126-a401-4c52-840a-012b1b197f19)
 
После редактирования:

 ![8](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/40bd0c02-8412-4bd4-b5d7-6703c8e10ba1)


4. Чтобы на форме со списком пациентов добавить столбец «Возраст», нужно создать свойство для модели PatientCard, которое будет выводить полный возраст пациента.
 
 ![9](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/b95d10e4-ed24-44df-9879-56b2c14ea658)
 
Затем в отображающем окне ListOfPatientsPage.xaml добавить новую колонку и привязать к ней свойство Age.

 ![10](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/1c0a05dd-0be7-4dde-9e9b-ec3543d2be13)
 
И обновить базу данных (update-database).

 ![11](https://github.com/mityaMz1998/TestProject_Junior/assets/100907269/ab2edb44-50de-493b-a73c-47c16e48eced)


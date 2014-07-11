OrmSimple
=========
Простя ОРМ потдерживает Mysql, Mssql.
Для инициализации при старте приложения
нужно инициализировать конфиг:
<pre><code class='language-cs'>
 new Configure(connectionString: @"Data Source=ION-PC\SQLEXPRESS;Initial Catalog=assa;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False",
                provider: ProviderName.MsSql,
                writeLog: true, //использование логироания
                logFileName: "E:/assa22.txt",
                usageCache: true);//использование кеша второго уровня
</code></pre>
Минимум аттрибутов и интерфейсов, поддерживает наследование, отсутствует авто валидация
и контент - еденица работы, объекты из базы не проксируются.

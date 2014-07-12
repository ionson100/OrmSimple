OrmSimple
=========
Простя ОРМ потдерживает Mysql, Mssql.
Мар на аттрибутах.
Для инициализации при старте приложения
нужно инициализировать конфиг:
<pre><code class='language-cs'>
 new Configure(connectionString: @"Data Source=ION-PC\SQLEXPRESS;Initial Catalog=assa;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False",
                provider: ProviderName.MsSql,
                writeLog: true, //использование логгирования
                logFileName: "E:/assa22.txt",
                usageCache: true);//использование кеша второго уровня, кеш первого уровня включен всегда
</code></pre>
Минимум аттрибутов и интерфейсов, поддерживает наследование, отсутствует авто валидация
и контент - еденица работы, объекты из базы не проксируются.
Запрос по верх кеша
<pre><code class='language-cs'>
  var l = ses.Querion<Telephone>().OverCache().Select(a => new { dd = a.Description }).ToList();
</code></pre>
Запрос хранимых процедур с параметрами
<pre><code class='language-cs'>
   var p1 = new ParameterStoredPr("p1", "qwqwqw", ParameterDirection.Input);
            var p2 = new ParameterStoredPr("p2", 2, ParameterDirection.Output);
            var res = ses.ProcedureCallParam<Body>("Assa2;", p1, p2).ToList();
</code></pre>
Рекомендации: Логгированием  пользоваться для отладки, кешируйте с умом, не стоит кешировать большие перечисления
что бы не попасть в LOH, чанков нет.

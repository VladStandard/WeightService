# Веб-сервис "Обмен с весовыми постами"
https://discord.com/channels/917316972818092073/1026439309731057724/1040252367842578505


## URL-адреса
[Предварительное тестирование](https://scales-dev-preview.kolbasa-vs.local:443/api/v3/)
[Рабочее тестирование](https://scales-dev.kolbasa-vs.local:443/api/v3/)
[Предварительный релиз](https://scales-prod-preview.kolbasa-vs.local:443/api/v3/)
[Рабочий релиз](https://scales-prod.kolbasa-vs.local:443/api/v3/)

## GET-запросы
[Инфо](https://<url>.kolbasa-vs.local:443/api/v3/info/)
[Нижний ШК](https://<url>.kolbasa-vs.local:443/api/v3/get_barcode/bottom?barcode=0112600076000000310300280011221109102211)
[Правый ШК](https://<url>.kolbasa-vs.local:443/api/v3/get_barcode/right/?barcode=2999876500000115)
[Верхний ШК](https://<url>.kolbasa-vs.local:443/api/v3/get_barcode/top?barcode=298987650000011522110911430111302800001)

## POST-запросы
Обмен справочником "Бренды": [https://<url>.kolbasa-vs.local:443/apiv3/send_brands/](https://<url>.kolbasa-vs.local:443/apiv3/send_brands/)
Обмен справочником "Номенклатура": [https://<url>.kolbasa-vs.local:443/apiv3/send_nomenclatures/](https://<url>.kolbasa-vs.local:443/apiv3/send_nomenclatures/)
Обмен справочником "Группы номенклатуры": [https://<url>.kolbasa-vs.local:443/apiv3/send_nomenclatures_groups/](https://<url>.kolbasa-vs.local:443/apiv3/send_nomenclatures_groups/)
Обмен справочником "Характеристики номенклатуры": [https://<url>.kolbasa-vs.local:443/apiv3/send_nomenclatures_characteristics/](https://<url>.kolbasa-vs.local:443/apiv3/send_nomenclatures_characteristics/)


## Шаблон ответа
```
<?xml version="1.0" encoding="utf-16"?>
<Response SuccessesCount="1" ErrorsCount="1" >
	<Successes>
		<Record Guid="f9562b74-2626-42cc-88d4-8061955d4736" />
	</Successes>
	<Errors>
		<Record Guid="592e10f6-7425-40c5-907e-fe1b1e9a3e91" Message="Error message" />
	</Errors>
</Response>
```


## Пример POST-запроса "Пустой тест без тела"
URL: 'https://<url>.kolbasa-vs.local:443/api/v3/send_test'
Content-Type: application/xml
## Пример ответа
```
<?xml version="1.0" encoding="utf-16"?>
<Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" SuccessesCount="0" ErrorsCount="0">
    <Successes>
        <Record>
            <Guid>a2357e19-4113-4ab5-be06-02af114741f3</Guid>
            <Message>Empty query. Try to make some select from any table.</Message>
        </Record>
    </Successes>
    <Errors />
</Response>
```


## Пример POST-запроса "ZPL-код этикетки по нижнему ШК"
URL: 'https://<url>.kolbasa-vs.local:443/api/v3/send_barcode/bottom/'
Content-Type: application/xml
Query body: 
```
<?xml version="1.0" encoding="utf-8"?>
<BarcodeBottom xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Const1>01</Const1>
    <Gtin>12600076000000</Gtin>
    <Const2>3103</Const2>
    <Weight>002800</Weight>
    <Const3>11</Const3>
    <Date>221109</Date>
    <Const4>10</Const4>
    <PartNumber>2211</PartNumber>
</BarcodeBottom>
```

## Пример POST-запроса "ZPL-код этикетки по правому ШК"
URL: 'https://<url>.kolbasa-vs.local:443/api/v3/send_barcode/right/'
Content-Type: application/xml
Query body: 
```
<?xml version="1.0" encoding="utf-8"?>
<BarcodeRight xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Const1>299</Const1>
    <ArmNumber>98765</ArmNumber>
    <Counter>00000115</Counter>
</BarcodeRight>
```


## Пример POST-запроса "ZPL-код этикетки по правому ШК"
URL: 'https://<url>.kolbasa-vs.local:443/api/v3/send_barcode/top/'
Content-Type: application/xml
Query body: 
```
<?xml version="1.0" encoding="utf-8"?>
<BarcodeTop xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Const1>298</Const1>
    <ArmNumber>98765</ArmNumber>
    <Counter>00000115</Counter>
    <Date>221109</Date>
    <Time>114301</Time>
    <Plu>113</Plu>
    <Weight>02800</Weight>
    <Zames>001</Zames>
    <Crc />
</BarcodeTop>
```

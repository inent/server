* 제품 속성
    * [제품 속성 전체 목록](#markdown-header-attribute-alllist)

    * [제품 속성 목록](#markdown-header-attribute-list)

    * [제품 속성 정보](#markdown-header-attribute-information)

    * [제품 속성 등록](#markdown-header-attribute-register)

    * [제품 속성 변경](#markdown-header-attribute-update)

    * [제품 속성 삭제](#markdown-header-attribute-remove)


### 제품 속성 ###
--------------------

-  

|기능           | Web Api                    | 권한  |  기타  |
|--------------:|:---------------------------|:------|:-------|
|속성 전체 목록 | `api/Attribs/alllist`         |       |        |
|속성 목록      | `api/Attribs/list`         |       |        |
|속성 정보      | `api/Attribs/info`         |       |        |
|등록           | `api/Attribs/register`     |       |        |
|갱신           | `api/Attribs/update`       |       |        |
|삭제           | `api/Attribs/remove`       |       |        |


### Attribute AllList ###
--------------------

- 속성 전체 목록

```html
    api/Attribs/alllist
```

- Request Http Post
```html
  nothing
```

- Response
```json
[
  {
    "id": 24,
    "productid": "",
    "type": "Sensor",
    "alias": "H2O",
    "name": "NH3",
    "onoff": "off",
    "label": "암모니아",
    "chemiunit": "ppm",
    "threshold": "30",
    "min": "2.5",
    "max": "50",
    "elecunit": "mV",
    "multiple": "5",
    "ratio": "0.2969",
    "constant": "-9.375",
    "resolution": "0.5",
    "deci": null
  },
  {
    "id": 33,
    "productid": "product-id-02",
    "type": "Actuator",
    "alias": "acc",
    "name": "acCurrent",
    "onoff": "",
    "label": "환기팬",
    "chemiunit": "mV",
    "threshold": "0",
    "min": "0",
    "max": "100",
    "elecunit": "",
    "multiple": "",
    "ratio": "",
    "constant": "",
    "resolution": "",
    "deci": ""
  },

]
```

### Attribute List ###
--------------------

- 제품 ID 에 대한 속성 목록을 받는다
- 제품 ID 를 전송한다

```html
    api/Attribs/list
```

- Request Http Post
```json
{
  "productid": ""
}
```

- Response
```json
[
  {
    "id": 32,
    "productid": "",
    "type": "Sensor",
    "alias": "nh3",
    "name": "NH3",
    "onoff": "off",
    "label": "암모니아",
    "chemiunit": "ppm",
    "threshold": "30",
    "min": "2.5",
    "max": "50",
    "elecunit": "mV",
    "multiple": "5",
    "ratio": "0.2969",
    "constant": "-9.375",
    "resolution": "0.5",
    "deci": null
  },
  {
    "id": 33,
    "productid": "",
    "type": "Actuator",
    "alias": "acc",
    "name": "acCurrent",
    "onoff": "",
    "label": "환기팬",
    "chemiunit": "mV",
    "threshold": "0",
    "min": "0",
    "max": "100",
    "elecunit": "",
    "multiple": "",
    "ratio": "",
    "constant": "",
    "resolution": "",
    "deci": ""
  },
]
```

### Attribute Information ###
--------------------

- 제품 속성 하나의 내용을 받는다
- 제품 속성 id 를 전송한다


```html
    api/Attribs/info
```

- Request Http Post
```json
{
  "id": 1
}
```

- Response
```json
{
  "id": 1,
  "productid": "product-id-01",
  "type": "Sensor",
  "alias": "nh3",
  "name": "NH3",
  "onoff": "off",
  "label": "암모니아",
  "spec": "",
  "chemiunit": "ppm",
  "threshold": "30",
  "min": "2.5",
  "max": "50",
  "elecunit": "mV",
  "note": ""
}
```


### Attribute Register ###
--------------------

- 속성 id 는 자동부여되므로 입력하지 않는다
- 속성 type 에 따라 항목이 다르다

```html
    api/Attribs/register
```

- Request Http Post
```json
{
  "productid": "product-id-01",
  "type": "Sensor",
  "alias": "nh3",
  "name": "NH3",
  "onoff": "off",
  "label": "암모니아",
  "spec": "",
  "chemiunit": "ppm",
  "threshold": "30",
  "min": "2.5",
  "max": "50",
  "elecunit": "mV",
  "note": ""
}
```

- Response
```json
{
  "result": "create"
}
```

### Attribute Update ###
--------------------

- 제품 속성 id 에 따른 내용을 수정한다

```html
    api/Attribs/update
```

- Request Http Post
```json
{
  "id": 40,
  "productid": "product-id-02",
  "type": "Sensor",
  "alias": "nh3",
  "name": "NH3",
  "onoff": "off",
  "label": "암모니아",
  "chemiunit": "ppm",
  "threshold": "30",
  "min": "2.5",
  "max": "50",
  "elecunit": "mV",
  "multiple": "5",
  "ratio": "0.2969",
  "constant": "-9.375",
  "resolution": "0.5",
  "deci": "1",
  "note": "update test",
  "area": ""
}
```

- Response
```json
{
  "result": "update success"
}
```

### Attribute Remove ###
--------------------

- 제품 속성 id 에 따른 내용을 삭제한다

```html
    api/Attribs/remove
```

- Request Http Post
```json
{
  "id": 7
}
```

- Response
```json
{
  "result": "removed"
}
```

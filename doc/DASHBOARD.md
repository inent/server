* 대시보드
    * [모니터링](#markdown-header-dashboard-monitoring)

    * [제품](#markdown-header-dashboard-products)

    * [제품 정보](#markdown-header-dashboard-product-info)

    * [장비](#markdown-header-dashboard-devicelist)

    * [전체 장비](#markdown-header-dashboard-devicealllist)

    * [알림사용현황](#markdown-header-dashboard-alert-usage)

    * [데이터 보기](#markdown-header-dashboard-chart)

    * [지자체주소정보](#markdown-header-dashboard-geocode)

    * [범위내 사용자](#markdown-header-dashboard-users4geo)

    * [현황](#markdown-header-dashboard-current-condition)


### Dashboard Monitoring ###
--------------------

- 

```html
    api/Menu/Monitoring
```

- Request Http Post or Get
```html
    nothing
```

- Response
```json
[
  {
    "deviceid": "device-id-01",
    "name": "장비-09",
    "odor": "98",
    "silution": "9",
    "solidity": "19",
    "h2s": "9.1",
    "nh3": "10.1",
    "voc": "11.1",
    "lati": "35.819196614526454",
    "longi": "127.1183289205119",
    "winddirect": "N",
    "windspeed": "9",
    "temperature": "19",
    "humidity": "29",
    "status": "on",
    "alert": "on"
  },
  {
    "deviceid": "device-id-02",
    "name": "장비-10",
    "odor": "99",
    "silution": "10",
    "solidity": "20",
    "h2s": "10.1",
    "nh3": "11.1",
    "voc": "12.1",
    "lati": "35.817065370411505",
    "longi": "127.11960480128178",
    "winddirect": "E",
    "windspeed": "10",
    "temperature": "20",
    "humidity": "30",
    "status": "off",
    "alert": "off"
  },
]
```


### Dashboard Products ###
--------------------

- 

```html
    api/Menu/Products
```

- Request Http Post or Get
```html
    nothing
```

- Response
```json
[
  {
    "id": "product-id-01",
    "name": "제품 이름 001",
    "company": "insys",	
    "regist": "2021-06-22",
    "release": "2020-12-21",
    "purpose": "용도",
    "note": "비고"
  },
  {
    "id": "product-jw-001",
    "name": "제품 이름 001",
    "company": "jtron",
    "regist": "2021-06-11",
    "release": "2020-12-21",
    "purpose": "용도",
    "note": "비고"
  },
]
```


### Dashboard Product Info ###
--------------------

- 

```html
    api/Menu/ProductInfo
```

- Request Http Post or Get
```html
{
  "id": "product-id-01"
}
```

- Response
```json
{
  "id": "product-id-01",
  "name": "제품 이름 001",
  "company": "insys",
  "attribs": [
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
    },
    {
      "id": 7,
      "productid": "product-id-01",
      "type": "Actuator",
      "alias": "acc",
      "name": "acCurrent",
      "onoff": "",
      "label": "환기팬",
      "spec": "",
      "chemiunit": "mV",
      "threshold": "0",
      "min": "0",
      "max": "100",
      "elecunit": "",
      "note": ""
    },
  ]
}
```


### Dashboard DeviceList ###
--------------------

- 

```html
    api/Menu/DeviceList
```

- Request Http Post or Get
```json
{
  "productid": "product-id-01"
}
```

- Response
```json
[
  {
    "id": 1,
    "name": "장비-01",
    "company": "insys",
    "control": "환풍기",
    "address": "전라북도 전주시 완산구 산월1길 32-5",
    "geocode": "1002",
    "status": "on",
    "nH3": [
      {
        "x": "20:25:26",
        "y": "4.00"
      },
    ],
    "h2S": [
      {
        "x": "20:25:26",
        "y": "8.45"
      },
    ],
    "odor": [
      {
        "x": "20:25:26",
        "y": "0.19"
      },
    ],
    "voc": [
      {
        "x": "20:25:26",
        "y": "3.95"
      },
    ]
  },
]
```


### Dashboard DeviceAllList ###
--------------------

- 

```html
    api/Menu/DeviceAllList
```

- Request Http Post or Get
```html
  nothing
```

- Response
```json
[
  {
    "id": 1,
    "name": "장비-01",
    "company": "insys",
    "control": "환풍기",
    "address": "전라북도 전주시 완산구 산월1길 32-5",
    "geocode": "1002",
    "status": "on",
    "nH3": [
      {
        "x": "20:25:26",
        "y": "4.00"
      },
    ],
    "h2S": [
      {
        "x": "20:25:26",
        "y": "8.45"
      },
    ],
    "odor": [
      {
        "x": "20:25:26",
        "y": "0.19"
      },
    ],
    "voc": [
      {
        "x": "20:25:26",
        "y": "3.95"
      },
    ]
  },
]
```


### Dashboard BoundsDevice ###
--------------------

- 

```html
    api/Menu/BoundsDevice
```

- Request Http Post or Get
```html
  nothing
```

- Response
```json
[
  {
    "id": 1,
    "name": "장비-01",
    "company": "insys",
    "control": "환풍기",
    "address": "전라북도 전주시 완산구 산월1길 32-5",
    "geocode": "1002",
    "status": "on"
  },
  {
    "id": 2,
    "name": "장비-02",
    "company": "insys",
    "control": "환풍기",
    "address": "전라북도 전주시 완산구 산월1길 32-3",
    "geocode": "1003",
    "status": "on"
  },
]
```

### Dashboard BoundsChart ###
--------------------

- 

```html
    api/Menu/BoundsChart
```

- Request Http Post or Get
```html
  nothing
```

- Response
```json
[
  {
	"x": "20:25:26",
	"nh3": "4.00",
	"h2s": "8.45",
	"odor": "0.19",
	"voc": "3.95"
  },
  {
	"x": "20:25:31",
	"nh3": "5.00",
	"h2s": "7.35",
	"odor": "1.19",
	"voc": "2.95"
  },
]
```


### Dashboard Alert Usage ###
--------------------

- 

```html
    api/Menu/alertusage
```

- Request Http Post or Get
```html
	nothing
```

- Response
```json
{
  "all": 13,
  "month": 0,
  "year": 13,
  "users": [
    {
      "id": 22,
      "userid": "id000",
      "name": "홍길동",
      "telno": "01012345678"
    },
    {
      "id": 23,
      "userid": "id000",
      "name": "홍길동",
      "telno": "01012345678"
    }
  ]
}
```


### Dashboard Chart ###
--------------------

- deviceid 는 배열형으로
- x: 실시간, 1분, 5분, 10분, 1시간, 일, 주, 월
- sect : rt / 1m / 5m / 10m / 1h / 1d / 1w / 1M
- y : 합계, 평균, 중앙값, 최소값, 최대값, 빈도수
- arith : sum / avg / ctr / min / max / frq
- limit : 받을 데이터 최대 갯수 


```html
    api/Menu/chart
```

- Request Http Post or Get
```json
{
  "deviceid": [
    "device-id-01",
    "device-id-02"
  ],
  "sect": "rt",
  "arith": "sum",
  "fromDate": "2021-07-01 22:04:02",
  "toDate": "2021-07-04 23:04:02",
  "limit":50
}
```

- Response
```json
OK
[
  {
    "deviceid": "device-id-01",
    "nH3": [
      {
        "x": "2021-07-04 23:08:28",
        "y": "4.78"
      },
    ],
    "h2S": [
      {
        "x": "2021-07-04 23:08:28",
        "y": "2.88"
      },
    ],
    "odor": [
      {
        "x": "2021-07-04 23:08:28",
        "y": "4.71"
      },
    ],
    "voc": [
      {
        "x": "2021-07-04 23:08:28",
        "y": "3.08"
      },
    ]
  },
  {
    "deviceid": "device-id-02",
  }
]
```



### Dashboard GeoCode ###
--------------------

- 

```html
    api/Menu/GeoCodeList
```

- Request Http Post or Get
```html
	nothing
```

- Response
```json
OK
[
  {
    "id": "1001",
    "name": "가평군",
    "metro": "경기도",
    "district": "군",
    "lati": "14190582.3660726",
    "longi": "4554351.5089590",
    "measure": "10",
    "exurl": "https://map.naver.com/v5/?c=14190582.3660726,4554351.5089590,10,0,0,0,dh"
  },
  {
    "id": "1002",
    "name": "강남구",
    "metro": "서울특별시",
    "district": "자치구",
    "lati": "14141239.8125353",
    "longi": "4508468.4918898",
    "measure": "12",
    "exurl": "https://map.naver.com/v5/search/%EA%B0%95%EB%82%A8%EA%B5%AC/address/14142863.229197372,4511459.828149432,%EC%84%9C%EC%9A%B8%ED%8A%B9%EB%B3%84%EC%8B%9C%20%EA%B0%95%EB%82%A8%EA%B5%AC,adm?c=14141289.7059311,4508468.5339821,12,0,0,0,dh"
  },
]
```


### Dashboard Users4Geo ###
--------------------

- 로그인 후 사용


```html
    api/Menu/users4geo
```

- Request Http Post or Get
```html
	nothing
```

- Response
```json
[
  {
    "userid": "id001",
    "username": "이름입니다",
	"depart": "회사002",
    "position": "팀장",
    "email": "id002@a.com",
    "phone": "010-1234-5678",
    "role": "admin",
	"geocode": "12300"
  }
]
```




### Dashboard Current Condition ###
--------------------

- 로그인 후 사용


```html
    api/Menu/currcond
```

- Request Http Post or Get
```html
	nothing
```

- Response
```json
{
  "alerts": [
    {
      "y": "2",
      "x": "2021-09-15"
    },
    {
      "y": "3",
      "x": "2021-09-16"
    },
    {
      "y": "1",
      "x": "2021-09-17"
    }
  ],
  "devnum": "14",
  "devon": "0",
  "nh3": "0",
  "h2s": "0",
  "odor": "0",
  "voc": "0",
  "indol": "0"
}
```

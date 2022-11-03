* 장비
    * [장비 전체 목록](#markdown-header-device-alllist)

    * [장비 목록](#markdown-header-device-list)
    
    * [장비 정보](#markdown-header-device-information)

    * [장비 등록](#markdown-header-device-register)

    * [장비 변경](#markdown-header-device-update)

    * [장비 삭제](#markdown-header-device-remove)

    
### 장비관리 ###
--------------------

-  

|기능            | Web Api                | 권한  |  기타  |
|---------------:|:-----------------------|:------|:-------|
|장비 전체 목록  | `api/Devices/alllist`  |       |        |
|장비 목록       | `api/Devices/list`     |       |        |
|장비 정보       | `api/Devices/info`     |       |        |
|장비 등록       | `api/Devices/register` |       |        |
|장비 변경       | `api/Devices/update`   |       |        |
|장비 삭제       | `api/Devices/remove`   |       |        |


### Device AllList ###
--------------------

- 전체 장비 목록

```html
    api/Devices/alllist
```

- Request Http Post
```html
    nothing
```

- Response
```json
[
  {
    "id": "246F28DAE774",
    "productid": "product-id-01",
    "name": "장비-04",
    "macaddr": "246F28DAE707",
    "addr": "전라북도 전주시 완산구",
    "depart": "농장이름",
    "geocode": "1002",
    "lati": "35.81019533971041",
    "longi": "127.12539729999241",
    "firmware": "3.1",
    "serverip": "175.208.89.113",
    "serverport": "7000",
    "memo": "진안 -4",
    "control": "안개분무",
    "status": "off",
    "on_nh3": false,
    "on_h2s": false,
    "on_odor": false,
    "on_voc": false,
    "on_indol": false,
    "on_temp": false,
    "on_humi": false,
    "on_sen1": false,
    "on_sen2": false,
    "on_sen3": false
  },
  {
    "id": "dc:a6:32:7b:24:b4",
    "productid": "product-id-01",
    "name": "무도장 함체",
    "macaddr": "246F28DAE706",
    "addr": "전라북도 전주시 완산구",
    "depart": "농장이름",
    "geocode": "1002",
    "lati": "35.82188645261162",
    "longi": "127.12243725668381",
    "firmware": "2.1",
    "serverip": "175.208.89.113",
    "serverport": "7000",
    "memo": "진안 -3",
    "control": "114호",
    "status": "on",
    "on_nh3": false,
    "on_h2s": false,
    "on_odor": false,
    "on_voc": false,
    "on_indol": false,
    "on_temp": false,
    "on_humi": false,
    "on_sen1": false,
    "on_sen2": false,
    "on_sen3": false
  }
]
```

### Device List ###
--------------------

- 제품 id 에 따른 장비 목록

```html
    api/Devices/list
```

- Request Http Post
```json
{
  "productid": "product-id-01"
}
```

- Response
```json
[
  {
    "id": "device-id-07",
    "productid": "product-id-02",
    "name": "장비-07",
    "macaddr": "246F28DAE710",
    "addr": "전라북도 전주시 완산구",
    "depart": "농장이름",
    "geocode": "1002",
    "lati": "35.81346488630303",
    "longi": "127.11835443815023",
    "firmware": "6.1",
    "serverip": "175.208.89.113",
    "serverport": "7000",
    "memo": "진안 -7",
    "control": "에어커튼",
    "status": "off",
    "on_nh3": false,
    "on_h2s": false,
    "on_odor": false,
    "on_voc": false,
    "on_indol": false,
    "on_temp": false,
    "on_humi": false,
    "on_sen1": false,
    "on_sen2": false,
    "on_sen3": false
  },
  {
    "id": "device-id-08",
    "productid": "product-id-02",
    "name": "장비-08",
    "macaddr": "246F28DAE711",
    "addr": "전라북도 전주시 완산구",
    "depart": "농장이름",
    "geocode": "1002",
    "lati": "35.82447274941943",
    "longi": "127.12190138666757",
    "firmware": "7.1",
    "serverip": "175.208.89.113",
    "serverport": "7000",
    "memo": "진안 -8",
    "control": "안개분무",
    "status": "off",
    "on_nh3": false,
    "on_h2s": false,
    "on_odor": false,
    "on_voc": false,
    "on_indol": false,
    "on_temp": false,
    "on_humi": false,
    "on_sen1": false,
    "on_sen2": false,
    "on_sen3": false
  }
]
```

### Device Information ###
--------------------

- 장비 정보 하나를 요청한다
- company 에 따라 항목이 다르다


```html
    api/Devices/info
```

- Request Http Post
```json
{
  "id": "device-id-01"
}
```
- Response
```json
{
  "id": "device-id-01",
  "productid": "product-id-01",
  "company": "insys",
  "name": "장비-01",
  "macaddr": "246F28DAE704",
  "addr": "전라북도 전주시 완산구",
  "depart": "농장이름",
  "geocode": "1002",
  "lati": "35.8116606",
  "longi": "127.1207626",
  "firmware": "0.1",
  "serverip": "175.208.89.113",
  "serverport": "7000",
  "memo": "진안 -1",
  "control": "환풍기",
  "status": "on",
  "on_nh3": false,
  "on_h2s": false,
  "on_odor": false,
  "on_voc": false,
  "on_indol": false,
  "on_temp": false,
  "on_humi": false,
  "on_sen1": false,
  "on_sen2": false,
  "on_sen3": false,
  "measect": "2",
  "meacycle": "6",
  "flushsect": "2",
  "restsect": "56",
  "multiple": "5",
  "ratio": "0.269",
  "constant": "-9.375",
  "resolution": "0.05",
  "deci": "2"
}
```

```json
{
  "id": "dc:a6:32:7b:24:b4",
  "productid": "Product-jw-01",
  "company": "jtron",
  "name": "장비-13",
  "macaddr": "246F28DAE716",
  "addr": "전라북도 전주시 완산구",
  "depart": "농장이름",
  "geocode": "1005",
  "lati": "38.81706537",
  "longi": "130.1196048",
  "firmware": "12.1",
  "serverip": "175.208.89.116",
  "serverport": "7003",
  "memo": "진안 -13",
  "control": "거품도포기",
  "status": "on",
  "on_nh3": true,
  "on_h2s": false,
  "on_odor": false,
  "on_voc": false,
  "on_indol": false,
  "on_temp": false,
  "on_humi": false,
  "on_sen1": false,
  "on_sen2": false,
  "on_sen3": false,
  "rex_nh3": "0.1 * exp( 0.2 * x )",
  "rex_h2s": "0.2*LN( x + 0.2)",
  "rex_odor": "2.2 * 10 * 0.3 * POW( x , 0.4)",
  "rex_voc": "2.2 * 10 * 0.3 * POW( x , 2)",
  "rex_ou": "2.2 * 10 * 0.3 * POW( x , 2)",
  "min1": "6",
  "min2": "4",
  "min3": "5",
  "min4": "6",
  "min5": "7",
  "rsvtime": "8",
  "rsvproc": "9",
  "odorlev": "10",
  "autoproc": "11"
}
```

### Device Register ###
--------------------

- 독립적인 id 를 반드시 넣어 요청해야 한다. 
- company 를 반드시 넣어야 한다
- company 에 따라 항목이 다르다


```html
    api/Devices/register
```

- Request Http Post
```json
{
  "id": "device-id-01",
  "productid": "product-id-01",
  "company": "insys",
  "name": "장비-01",
  "macaddr": "246F28DAE704",
  "addr": "전라북도 전주시 완산구",
  "depart": "농장이름",
  "geocode": "1002",
  "lati": "35.8116606",
  "longi": "127.1207626",
  "firmware": "0.1",
  "serverip": "175.208.89.113",
  "serverport": "7000",
  "memo": "진안 -1",
  "control": "환풍기",
  "status": "on",
  "on_nh3": false,
  "on_h2s": false,
  "on_odor": false,
  "on_voc": false,
  "on_indol": false,
  "on_temp": false,
  "on_humi": false,
  "on_sen1": false,
  "on_sen2": false,
  "on_sen3": false,
  "measect": "2",
  "meacycle": "6",
  "flushsect": "2",
  "restsect": "56",
  "multiple": "5",
  "ratio": "0.269",
  "constant": "-9.375",
  "resolution": "0.05",
  "deci": "2"
}
```

```json
{
  "id": "dc:a6:32:7b:24:b4",
  "productid": "Product-jw-01",
  "company": "jtron",
  "name": "장비-13",
  "macaddr": "246F28DAE716",
  "addr": "전라북도 전주시 완산구",
  "depart": "농장이름",
  "geocode": "1005",
  "lati": "38.81706537",
  "longi": "130.1196048",
  "firmware": "12.1",
  "serverip": "175.208.89.116",
  "serverport": "7003",
  "memo": "진안 -13",
  "control": "거품도포기",
  "status": "on",
  "on_nh3": true,
  "on_h2s": false,
  "on_odor": false,
  "on_voc": false,
  "on_indol": false,
  "on_temp": false,
  "on_humi": false,
  "on_sen1": false,
  "on_sen2": false,
  "on_sen3": false,
  "rex_nh3": "0.1 * exp( 0.2 * x )",
  "rex_h2s": "0.2*LN( x + 0.2)",
  "rex_odor": "2.2 * 10 * 0.3 * POW( x , 0.4)",
  "rex_voc": "2.2 * 10 * 0.3 * POW( x , 2)",
  "rex_ou": "2.2 * 10 * 0.3 * POW( x , 2)",
  "min1": "6",
  "min2": "4",
  "min3": "5",
  "min4": "6",
  "min5": "7",
  "rsvtime": "8",
  "rsvproc": "9",
  "odorlev": "10",
  "autoproc": "11"
}
```

- Response
```json
{
  "result": "create"
}
```

### Device Update ###
--------------------

- company 를 반드시 넣어야 한다
- company 에 따라 항목이 다르다
- info 로 얻은 데이터 항목 그대로 내용 변경해서 전송이 바람직하다

```html
    api/Devices/update
```

- Request Http Post
```json
{
  "id": "device-id-01",
  "productid": "product-id-01",
  "company": "insys",
  "name": "장비-01",
  "macaddr": "246F28DAE704",
  "addr": "전라북도 전주시 완산구",
  "depart": "농장이름",
  "geocode": "1002",
  "lati": "35.8116606",
  "longi": "127.1207626",
  "firmware": "0.1",
  "serverip": "175.208.89.113",
  "serverport": "7000",
  "memo": "진안 -1",
  "control": "환풍기",
  "status": "on",
  "on_nh3": false,
  "on_h2s": false,
  "on_odor": false,
  "on_voc": false,
  "on_indol": false,
  "on_temp": false,
  "on_humi": false,
  "on_sen1": false,
  "on_sen2": false,
  "on_sen3": false,
  "measect": "2",
  "meacycle": "6",
  "flushsect": "2",
  "restsect": "56",
  "multiple": "5",
  "ratio": "0.269",
  "constant": "-9.375",
  "resolution": "0.05",
  "deci": "2"
}
```

```json
{
  "id": "dc:a6:32:7b:24:b4",
  "productid": "Product-jw-01",
  "company": "jtron",
  "name": "장비-13",
  "macaddr": "246F28DAE716",
  "addr": "전라북도 전주시 완산구",
  "depart": "농장이름",
  "geocode": "1005",
  "lati": "38.81706537",
  "longi": "130.1196048",
  "firmware": "12.1",
  "serverip": "175.208.89.116",
  "serverport": "7003",
  "memo": "진안 -13",
  "control": "거품도포기",
  "status": "on",
  "on_nh3": true,
  "on_h2s": false,
  "on_odor": false,
  "on_voc": false,
  "on_indol": false,
  "on_temp": false,
  "on_humi": false,
  "on_sen1": false,
  "on_sen2": false,
  "on_sen3": false,
  "rex_nh3": "0.1 * exp( 0.2 * x )",
  "rex_h2s": "0.2*LN( x + 0.2)",
  "rex_odor": "2.2 * 10 * 0.3 * POW( x , 0.4)",
  "rex_voc": "2.2 * 10 * 0.3 * POW( x , 2)",
  "rex_ou": "2.2 * 10 * 0.3 * POW( x , 2)",
  "min1": "6",
  "min2": "4",
  "min3": "5",
  "min4": "6",
  "min5": "7",
  "rsvtime": "8",
  "rsvproc": "9",
  "odorlev": "10",
  "autoproc": "11"
}
```

- Response
```json
{
  "result": "update success"
}
```


### Device Remove ###
--------------------

- id 를 전송하고 삭제 성공하면 삭제된 데이터가 돌아온다

```html
    api/Devices/remove
```

- Request Http Post
```json
{
  "id": "device-id-01"
}
```

- Response
```json
{
  "result": "removed"
}
```


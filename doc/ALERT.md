### 알림설정     ###
--------------------

* 알림
    * [알림 목록](#markdown-header-alert-list)

    * [알림 정보](#markdown-header-alert-information)

    * [알림 등록](#markdown-header-alert-register)

    * [알림 변경](#markdown-header-alert-update)

    * [알림 삭제](#markdown-header-alert-remove)

    * [알림 이력](#markdown-header-alert-history)

    * [현재 받을 알림](#markdown-header-alert-todolist)

    * [알림 사용자 목록](#markdown-header-alert-user-list)

    * [알림 사용자 추가](#markdown-header-alert-user-add)

    * [알림 사용자 삭제](#markdown-header-alert-user-delete)


-  

|기능              | Web Api               | 구현   |  기타  |
|-----------------:|:----------------------|:-------|:-------|
|알림 목록         | `api/Alerts/list`     |        |        |
|알림 정보         | `api/Alerts/info`     |        |        |
|알림 등록         | `api/Alerts/register` |        |        |
|알림 정보변경     | `api/Alerts/update`   |        |        |
|알림 삭제         | `api/Alerts/remove`   |        |        |
|알림 이력         | `api/Alerts/history`  |        |        |
|현재 받을 알림    | `api/Alerts/todoList` |        |        |
|알림 사용자 목록  | `api/Alerts/userlist` |        |        |
|알림 사용자 추가  | `api/Alerts/useradd`  |        |        |
|알림 사용자 삭제  | `api/Alerts/userdel`  |        |        |


### Alert List ###
--------------------

```html
    api/Alerts/List
```

- Request Http Post
```json
    nothing
```

- Response
```json
[
  {
    "id": 1,
    "part": "소속-01",
    "name": "장비이름-01",
    "type": "센서종류-01",
    "warn": "경고수치",
    "err": "위험수치",
    "setup": "설정상태"
  },
  {
    "id": 3,
    "part": "소속-01",
    "name": "장비이름-01",
    "type": "센서종류-01",
    "warn": "경고수치",
    "err": "위험수치",
    "setup": "설정상태"
  }
]
```


### Alert Information ###
--------------------

```html
    api/Alerts/info
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
  "part": "소속-01",
  "name": "장비이름-01",
  "type": "센서종류-01",
  "warn": "경고수치",
  "err": "위험수치",
  "setup": "설정상태"
}
```

### Alert Register ###
--------------------

- id 는 자동부여된다

```html
    api/Alerts/register
```

- Request Http Post
```json
{
  "part": "소속-01",
  "name": "장비이름-01",
  "type": "센서종류-01",
  "warn": "경고수치",
  "err": "위험수치",
  "setup": "설정상태"
}
```

- Response
```json
{
  "result": "create"
}
```

### Alert Update ###
--------------------

```html
    api/Alerts/update
```

- Request Http Post
```json
{
  "id": 1,
  "part": "소속-01",
  "name": "장비이름-01",
  "type": "센서종류-01",
  "warn": "경고수치",
  "err": "위험수치",
  "setup": "설정상태"
}
```

- Response
```json
{
  "result": "update success"
}
```

### Alert Remove ###
--------------------

```html
    api/Alerts/remove
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
  "result": "removed"
}
```

### Alert History ###
--------------------

- 검색 시작과 마지막 시간이 없으면 전체 리스트
- 검색 범위 시간 포맷은 yyyy-MM-dd HH:mm:ss


```html
    api/Alerts/history
```

- Request Http Post
```json
{
  "page" : 1,
  "sizePerPage" : 3,
  "sortField" : "",
  "sortOrder" : "desc",
  "search" : "",
  "fromDate": "검색 시작 시간",
  "toDate": "검색 마지막 시간"
}
```

- Response
```json
{
  "totalSize": 6,
  "pagedList": [
    {
      "id": 8,
      "status": "pending",
      "type": "fatal",
      "name": "장비4",
      "kind": "통신OFF",
      "content": "통신두절",
      "value": "ON/OFF",
      "times": "2021-06-02T07:57:00"
    },
    {
      "id": 7,
      "status": "pending",
      "type": "fatal",
      "name": "장비3",
      "kind": "전원",
      "content": "장비고장",
      "value": "ON/OFF",
      "times": "2021-06-01T07:57:00"
    },
    {
      "id": 6,
      "status": "pending",
      "type": "error",
      "name": "",
      "kind": "NH3센서",
      "content": "응답없음",
      "value": "0",
      "times": "2021-05-31T07:57:00"
    }
  ]
}
```

### Alert todoList ###
--------------------

- 로그인 아이디로 등록된 alertid의 10초 전 목록을 반환


```html
    api/Alerts/todoList
```

- Request Http Post
```html
	nothing
```

- Response
```json
[
  {
    "id": 8,
    "alertid": 2,
	"userid":"id000",
    "status": "pending",
    "type": "fatal",
    "name": "장비4",
    "kind": "통신OFF",
    "content": "통신두절",
    "value": "ON/OFF",
    "times": "2021-06-02T07:57:00"
  },
  {
    "id": 9,
    "alertid": 3,
	"userid":"id000",
    "status": "pending",
    "type": "warn",
    "name": "장비3",
    "kind": "전원",
    "content": "장비고장",
    "value": "ON",
    "times": "2021-06-22T22:11:27.2238089"
  }
]
```

### Alert User List ###
--------------------

-


```html
    api/Alerts/userlist
```

- Request Http Post
```html
	nothing
```

- Response
```json
[
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
```

### Alert User Add ###
--------------------

-


```html
    api/Alerts/useradd
```

- Request Http Post
```json
{
  "userid": "id000",
  "name": "홍길동",
  "telno": "01012345678"
}
```

- Response
```json
{
  "result": "add"
}
```

### Alert User Delete ###
--------------------

-


```html
    api/Alerts/userdel
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
  "result": "removed"
}
```

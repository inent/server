
* [로그인](#markdown-header-log-in)


* 사용자
    * [사용자 등록](#markdown-header-user-register)

    * [사용자 확인](#markdown-header-user-confirm)

    * [사용자 목록](#markdown-header-user-list)

    * [사용자 정보](#markdown-header-user-information)

    * [사용자 변경](#markdown-header-user-update)

    * [사용자 삭제](#markdown-header-user-remove)

    * [사용자 권한](#markdown-header-user-role)

    * [권한 지역](#markdown-header-user-geocode)

    * [암호 재발급](#markdown-header-new-password)


## 로그인 ##
--------------------

-  

|기능     | Web Api           | 구현 | 기타   |
|--------:|:----------------- |:-----|:-------|
|로그인   | `api/Users/Login` | 성공 |        |


### Log-In ###
--------------------

```html
  api/Login
```

- Request Http Post
```json
{
  "userid": "user_id",
  "userpw": "user_password"
}
```

- Response
```json
{
  "userid": "id001",
  "username": "이름001",
  "depart": "회사001",
  "position": "사장",
  "email": "id001@a.com",
  "phone": "010-1234-5678",
  "role": "super",
  "token": "eyJhbGciOi...",
  "onweb": true,
  "onmail": false,
  "onsms": false,
  "onpush": false
}
```



### 사용자관리     ###
--------------------

-  

|기능            | Web Api                 | 권한   |  기타                 |
|---------------:|:------------------------|:-------|:----------------------|
|사용자 등록     | `api/Users/register`    | 사용자 |                       |  
|사용자 확인     | `api/Users/confirm`     | 관리자 |                       |  
|사용자 목록     | `api/Users/UserList`    | 사용자 |                       |
|사용자 정보     | `api/Users/info`        | 사용자 |                       |
|사용자 정보변경 | `api/Users/update`      | 사용자 |                       |
|사용자 삭제     | `api/Users/remove`      | 관리자 |                       |
|사용자 권한등록 | `api/Users/setRole`     | 관리자 |                       |
|사용자 지역     | `api/Users/setGeoCode`  | 관리자 |                       |
|암호 재발급     | `api/Users/newpassword` | 사용자 |                       |  



### User Register ###
--------------------

- userid, email 필수
- 설정된 기본 관리자 email 로 확인 메일이 발송된다

```html
    api/Users/register
```

- Request Http Post
```json
{
  "userid": "id001",
  "username": "이름001",
  "depart": "회사001",
  "position": "팀장",
  "email": "id001@a.com",
  "phone": "010-1234-5678"
}
```

- Response
```json
{
  "id": 11
}
```

### User Confirm ###
--------------------

- userid 필수
- 설정된 email 로 확인 메일이 발송된다

```html
    api/Users/confirm
```

- Request Http Post
```json
{
  "userid": "id001"
}
```

- Response
```json
{
  "result": "confirmed."
}
```

### User List ###
--------------------

```html
    api/Users/UserList
```

- Request Http Post
```json
    nothing
```

- Response
```json
[
  {
    "userid": "id001",
    "username": "이름001",
    "depart": "회사001",
    "position": "사장",
    "email": "id001@a.com",
    "phone": "010-1234-5678",
    "role": "super",
	"geocode": "12300"
  },
  {
    "userid": "id002",
    "username": "이름002",
    "depart": "회사002",
    "position": "팀장",
    "email": "id002@a.com",
    "phone": "010-1234-5678",
    "role": "admin",
	"geocode": "12300"
  },
  ...
]
```

### User Information ###
--------------------

```html
    api/Users/info
```

- Request Http Post
```json
{
  "userid": "id001"
}
```

- Response
```json
{
  "userid": "id001",
  "username": "이름111",
  "depart": "회사001",
  "position": "팀장",
  "email": "firesaba@inent.co.kr",
  "phone": "010-1234-5678",
  "role": "user",
  "onweb": true,
  "onmail": false,
  "onsms": false,
  "onpush": false,
  "isConfirm": false
}
```

### User Update ###
--------------------

- 로그인된 사용자의 정보를 변경한다

```html
    api/Users/update
```

- Request Http Post
```json
{
  "userpw": "id001",
  "username": "이름001",
  "depart": "회사001",
  "position": "팀장",
  "email": "firesaba@inent.co.kr",
  "phone": "010-1234-5678",
  "onweb": false,
  "onmail": false,
  "onsms": false,
  "onpush": false
}
```

- Response
```json
{
  "result" : "changed."
}
```

### User Remove ###
--------------------

- 관리자기능

```html
    api/Users/remove
```

- Request Http Post
```json
{
  "userid": "id001"
}
```

- Response
```json
{
  "result" : "이름001 removed"
}
```


### User Role ###
--------------------

- 관리자기능

```html
    api/Users/setRole
```

- Request Http Post
```json
{
  "userid": "id001",
  "role": "admin"
}
```

- Response
```json
{
  "result": "이름001 set Role admin"
}
```

### User GeoCode ###
--------------------

- 관리자기능

```html
    api/Users/setGeoCode
```

- Request Http Post
```json
{
  "userid": "id001",
  "geocode": "1003"
}
```

- Response
```json
{
  "result": "geocode Changed"
}
```

### New Password ###
--------------------

- 

```html
    api/Users/newpassword
```

- Request Http Post
```json
{
  "userid": "id001",
  "username": "이름001",
  "email": "firesaba@inent.co.kr"
}
```

- Response
```json
{
  "result": "reissued password",
  "id": "id001"
}
```


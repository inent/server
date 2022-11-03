* 제품
    * [제품 목록](#markdown-header-product-list)

    * [제품 정보](#markdown-header-product-information)

    * [제품 등록](#markdown-header-product-register)

    * [제품 변경](#markdown-header-product-update)

    * [제품 삭제](#markdown-header-product-remove)


### 제품 ###
--------------------

-  

|기능     | Web Api                    | 권한  |  기타  |
|--------:|:---------------------------|:------|:-------|
|목록     | `api/Products/list`        |       |        |
|정보     | `api/Products/info`        |       |        |
|등록     | `api/Products/register`    |       |        |
|갱신     | `api/Products/update`      |       |        |
|삭제     | `api/Products/remove`      |       |        |


### Product List ###
--------------------

- 

```html
    api/Products/List
```

- Request Http Post
```html
	nothing
```

- Response
```json
[
  {
    "id": "product-id-011",
    "name": "제품 이름 001",
    "company": "insys",
    "regist": "2021-06-11",
    "release": "2020-12-21",
    "purpose": "용도",
    "note": "비고"
  },
  {
    "id": "product-id-02",
    "name": "제품 이름 2",
    "company": "JTRON",
    "regist": "2020-06-02",
    "release": null,
    "purpose": null,
    "note": null
  }
]
```


### Product Information ###
--------------------

- 제품 하나의 내용을 받는다
- 제품 id 를 전송한다

```html
    api/Products/info
```

- Request Http Post
```json
{
  "id": "product-id-01"
}
```

- Response
```json
{
  "id": "product-id-01",
  "name": "제품 이름 001",
  "regist": "2021-06-11",
  "release": "2020-12-21",
  "purpose": "용도",
  "note": "비고"
}
```


### Product Register ###
--------------------

- id 가 중복되면 안된다

```html
    api/Products/register
```

- Request Http Post
```json
{
  "id": "product-id-03",
  "name": "제품 이름 001",
  "regist": "2021-06-11",
  "release": "2020-12-21",
  "purpose": "용도",
  "note": "비고"
}
```

- Response
```json
{
  "result": "created"
}
```

### Product Update ###
--------------------

- 제품 id 에 따른 내용을 수정한다

```html
    api/Products/update
```

- Request Http Post
```json
{
  "id": "product-id-011",
  "name": "제품 이름 001",
  "regist": "2021-06-11",
  "release": "2020-12-21",
  "purpose": "용도",
  "note": "비고"
}
```

- Response
```json
{
  "result": "update success"
}
```

### Product Remove ###
--------------------

- 제품 id 에 따른 내용을 삭제한다

```html
    api/Products/remove
```

- Request Http Post
```json
{
  "id": "product-id-05"
}
```

- Response
```json
{
  "result": "removed"
}
```


* [접속자 확인](#markdown-header-connection-list)

* [명령어 전송](#markdown-header-command-sender)


### Connection List ###
--------------------


- Request Http Post
```json
{
	"command": "connectList",
	"deviceId": "접속자 id"
}
```

- Response
```json
{
	"res": "connectList",
	"cnt": 2,
	"arr": [
		"dc:a6:32:e4:e6:1f",
		"접속자 id"
	],
	"rsvcnt": 0
}

```


### Command sender ###
--------------------


- Request
```json
{
	"command": "orderJtron",
	"deviceId": "dc:a6:32:7b:24:b4",
	"orderid": "admin-device-id-202",
	"ordermsg": "{\"company\":\"JTRON\",\"name\":\"server\",\"deviceId\":\"dc:a6:32:7b:24:b4\",\"sendDt\":\"2021-08-22 09:56:22\",\"type\":\"sysInfo\"}"
}
```

- Response
```json
{
	"company": "JTRON",
	"name": "client",
	"deviceId": "dc:a6:32:e4:e6:1f",
	"sendDt": "2021-08-22 09:37:42",
	"type": "sysInfo",
	"sysInfo": {
		"fwVersion": "V1.0003",
		"cntProcess": 0,
		"reservedDateTime": "2000-01-01 00:00:01",
		"reservedProcess": 0,
		"autoProcOdorLev": 1.23,
		"autoProcess": 0,
		"sampled": 0,
		"sampleStartTime": "2021-01-01 00:00:00",
		"sampleLabTime": "00:00:00"
	}
}
```


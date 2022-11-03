* [수식 검사](#markdown-header-check-formula)


### Check Formula ###
--------------------

- 

```html
    api/Test/CheckFormula
```

- Request Http Post or Get
```json
{
  "rex_nh3": "0.2*10*0.3*POW( x ,0.4)"
}
```

- Response
```json
{
  "nh3": "0.3384623852768095"
}

{
  "result": "Unknown column 'x' in 'field list'"
}
```

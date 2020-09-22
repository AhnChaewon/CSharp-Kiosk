# CSharp-Kiosk

#### 커밋 규칙

- [ADD] : 파일이나 기능, 함수 등을 추가할 때
- [DELETE] : 파일이나 기능, 함수 등을 삭제할 때
- [UPDATE] : 파일이나 기능, 함수 등을 수정할 때

#### 띄어쓰기 및 코드 정리

- 키워드, 연산자와 다른 코드 사이에 공백이 있어야 한다
- 한 줄에 25자가 넘지 않도록 한다. (한 눈에 보기 쉽게 한다.)
- 접근이 길어질 때는 .을 기준으로 줄을 바꾼다

```C#
int a=0 //BAD
int a = 0 //GOOD

builder.create().checker("JUSTTEXTANYTHING").finish(BLAHBLAH_TYPE) //BAD

builder.create()
  .checker("JUSTTEXTANYTHING")
  .finish(BLAHBLAH_TYPE) //GOOD
```

#### 명명

- 변수와 함수는 카멜 케이스를 사용
- 클래스나 파일은 파스칼 케이스를 사용한다.
- 상수는 전부 대문자로 적고 스네이크 케이스를 사용
- (참고 자료) https://m.blog.naver.com/PostView.nhn?blogId=playcodingacademy&logNo=221317135750&proxyReferer=https:%2F%2Fwww.google.com%2F

```C#
int helloWorld
String KEY_WORDS
```

#### if문, for문 등의 제어문

- 한 줄에 끝나는 문은 중괄호 없이 옆에 적는다.
- if문 같은 경우 한 줄로 끝나도 else나 else if가 한 줄이 아니면 중괄호를 쓴다.
- 조건 안의 변수와 연산자마다 뛰어쓰기가 되어있어야 한다.

```C#
if(a != b) c = a //GOOD
if(a!=b) c = a //BAD

if(a != b) c = a
else{
  c = b
  b = d
}                 //BAD

if(a != b) {
  c = a
}
else{
  c = b
  b = d
}                 //GOOD
```

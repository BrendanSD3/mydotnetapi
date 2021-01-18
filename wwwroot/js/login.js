const uri = 'api/User';
let users=[];

function login(){
    console.log("login");
    fetch(uri)
    .then(response => response.json())
    .then(data => doCheck(data))
    .catch(error => console.error('Unable to get items.', error));

   }
function doCheck(data)
{
    users=data;
    const name=document.getElementById("login").nodeValue.trim();
    const pass=document.getElementById("password").nodeValue.trim();
    const user = users.find(user => user.username === name);
    const passw= users.find(user => user.pass === pass);
    if(user!=null&&passw!=null)
    {
        window.location.replace('/CarStore.html');
    }
}   
function compare()
{
    const name=document.getElementById("login").nodeValue.trim();
    const pass=document.getElementById("password").nodeValue.trim();
    

    fetch(uri, {
        method:'GET',
        
    }).then(response=>response.json())
    .then(json=>console.log(json))
    .catch(error => console.error('Unable to delete item.', error));

}
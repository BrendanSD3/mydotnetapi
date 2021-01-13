const uri = 'api/Cars';
const weather='api/WeatherForecast';
let cars = [];

function getall(){
 // Calls the Todoitems api logs the response and then calls the weather api
    fetch(uri) .then(response => response.json())
    .then(json => console.log(json)),
    fetch(weather) .then(response => response.json())
    .then(json => console.log(json));
  }

function getCars() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}
/* When the web API returns a successful status code, 
the _displayItems function is invoked.
 Each to-do item in the array parameter accepted by _displayItems is added to a table with Edit and Delete buttons. 
 If the web API request fails, an error is logged to the browser's console.
 */
 function addCar() {
  const addMakeTextbox = document.getElementById('add-make');
  const addModelTextbox = document.getElementById('add-model');
  const addPrice = document.getElementById('add-price');
  const addYear = document.getElementById('add-year');

  const car = {
    make:addMakeTextbox.value.trim(),
    model: addModelTextbox.value.trim(),
    price:addPrice.value,
    year:addYear.value
    };

  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(car)
  })
    .then(response => response.json())
    .then(() => {
      getCars();
      addMakeTextbox.value = '';
      addModelTextbox.value='';
      addPrice.value='';
      addYear.value='';
      document.getElementById('itemadded').style.display='block';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
  fetch(`${uri}/${id}`, {
    method: 'DELETE'
  })
  .then(() => getCars())
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
  const car = cars.find(car => car.id === id);
  
  document.getElementById('edit-id').value = car.id;
  document.getElementById('edit-model').value = car.model;
  document.getElementById('edit-make').value = car.make;
  document.getElementById('edit-year').value = car.year;
  document.getElementById('edit-price').value = car.price;

  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const carId = document.getElementById('edit-id').value;
  const car = {
    id: parseInt(carId, 10),
    make : document.getElementById('edit-make').value.trim(),
    model: document.getElementById('edit-model').value.trim(),
    price: document.getElementById('edit-price').value,
    year: document.getElementById('edit-year').value
  };

  fetch(`${uri}/${carId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(car)
  })
  .then(() => getCars())
  .catch(error => console.error('Unable to update item.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'Car' : 'Cars';

  document.getElementById('count').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
   const tBody = document.getElementById('cars');
  tBody.innerHTML = '';
  console.log(data.length);
  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(car => {
   
    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit ';
    editButton.setAttribute('class','btn btn-primary ');
    let editicon =document.createElement('i');
    editicon.setAttribute('class', 'bi bi-pencil-square');
    editButton.appendChild(editicon);   
    editButton.setAttribute('onclick', `displayEditForm(${car.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete ';
    deleteButton.setAttribute('class','btn btn-danger');
    let icon =document.createElement('i');
    icon.setAttribute('class', 'fa fa-trash');

    deleteButton.appendChild(icon);
    
    deleteButton.setAttribute('onclick', `deleteItem(${car.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    let id = document.createTextNode(car.id);
    td1.appendChild(id);

    let td2 = tr.insertCell(1);
    let make = document.createTextNode(car.make);
    td2.appendChild(make);

    let td3 = tr.insertCell(2);
    let model = document.createTextNode(car.model);
    td3.appendChild(model);

    let td4 = tr.insertCell(3);
    let price = document.createTextNode("€"+car.price);
    td4.appendChild(price);

    let td5 = tr.insertCell(4);
    let Year= document.createTextNode(car.year);
    td5.appendChild(Year);
    let td6 = tr.insertCell(5);
    td6.appendChild(editButton);

    let td7 = tr.insertCell(6);
    td7.appendChild(deleteButton); 
  });

  cars = data;
}
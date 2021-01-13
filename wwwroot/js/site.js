const uri = 'api/TodoItems';
const weather='api/WeatherForecast';
let todos = [];

function getall(){
 // Calls the Todoitems api logs the response and then calls the weather api
    fetch(uri) .then(response => response.json())
    .then(json => console.log(json)),
    fetch(weather) .then(response => response.json())
    .then(json => console.log(json));
  }

function getItems() {
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
 function addItem() {
  const addNameTextbox = document.getElementById('add-name');

  const item = {
    isComplete: false,
    name: addNameTextbox.value.trim()
  };

  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      addNameTextbox.value = '';
     
      document.getElementById('itemadded').style.display='block';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
  fetch(`${uri}/${id}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
  const item = todos.find(item => item.id === id);
  
  document.getElementById('edit-name').value = item.name;
  document.getElementById('edit-id').value = item.id;
  document.getElementById('edit-isComplete').checked = item.isComplete;
  document.getElementById('editForm').style.display = 'block';
}



// Function to update when a checkbox is ticked or unticked 
function testcheckbox(id){
    //console.log("Something changed with id number: ", id);
    const item = todos.find(item => item.id === id);
    checkbox=document.getElementById(id);
    
    if(checkbox.checked==true)
    {
        item.isComplete=true;
        //console.log("checked" , item.isComplete);
        updateCheckbox(item);
        const element1=document.getElementById("completed");
        element1.classList.add('animate__animated','animate__heartBeat');
        element1.classList.remove('animate__animated','animate__heartBeat');
        void element1.offsetWidth;
        element1.classList.add('animate__animated','animate__heartBeat');
        //element1.classList.remove('animate__animated','animate__heartBeat');
    }
    else{
        item.isComplete=false;
        updateCheckbox(item);
        const element=document.getElementById("incomplete");
        element.classList.add('animate__animated','animate__heartBeat');
        element.classList.remove('animate__animated','animate__heartBeat');
        void element.offsetWidth;
        element.classList.add('animate__animated','animate__heartBeat');
        
       

    }
}

function updateCheckbox(item) {
    //console.log("in updatecheckbox", item);

    const itemId=item.id;
  
    fetch(`${uri}/${itemId}`, {
      method: 'PUT',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(item)
    })
    .then(() => getItems())
    .catch(error => console.error('Unable to update item.', error));
   
    return false; 
  }





function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    id: parseInt(itemId, 10),
    isComplete: document.getElementById('edit-isComplete').checked,
    name: document.getElementById('edit-name').value.trim()
  };

  fetch(`${uri}/${itemId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to update item.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'to-do' : 'to-dos';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
 
  var completecount=0;
  var incompletecount=0;
  for(var i=0; i<data.length;i++)
  {
    if(data[i].isComplete==true)
    {
      completecount++;
      

     // console.log("iscomplete is true", data[i]);
    }
  else{
    incompletecount++;
    //console.log("iscomplete is false");
  }
  }
  document.getElementById('completed').innerHTML=completecount;
  document.getElementById('incomplete').innerHTML=incompletecount;
  const tBody = document.getElementById('todos');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
    let isCompleteCheckbox = document.createElement('input');
    isCompleteCheckbox.type = 'checkbox';
    isCompleteCheckbox.disabled = false;
    isCompleteCheckbox.checked = item.isComplete;
    isCompleteCheckbox.setAttribute('onchange', `testcheckbox(${item.id})`);
    isCompleteCheckbox.setAttribute('id', `${item.id}`);
    isCompleteCheckbox.setAttribute('class', 'form-check-input')

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    td1.appendChild(isCompleteCheckbox);

    let td2 = tr.insertCell(1);
    let textNode = document.createTextNode(item.name);
    td2.appendChild(textNode);

    let td3 = tr.insertCell(2);
    td3.appendChild(editButton);

    let td4 = tr.insertCell(3);
    td4.appendChild(deleteButton);
  });

  todos = data;
}
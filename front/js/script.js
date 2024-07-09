document.addEventListener("DOMContentLoaded", function() {
    cargarItems(); // carga todos los items al cargar la página
});

function cargarItems(filtro = '') {
    let url = 'https://localhost:44373/api/Items/GetAll';
    if (filtro) {
        url += `?nombre=${encodeURIComponent(filtro)}`;
    }

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            const tabla = document.getElementById('tabla').getElementsByTagName('tbody')[0];
            tabla.innerHTML = ''; // limpia la tabla antes de añadir nuevos datos
            data.forEach(item => {
                let fila = tabla.insertRow();
                fila.insertCell(0).textContent = item.Nombre;
                fila.insertCell(1).textContent = item.Descripcion;
                let btnEliminar = document.createElement('button');
                btnEliminar.textContent = 'Eliminar';
                btnEliminar.onclick = function() { eliminar(item.Id); };
                fila.insertCell(2).appendChild(btnEliminar);
            });
        })
        .catch(error => console.error('Error:', error));
}


function buscar() {
    const filtro = document.getElementById('filtro').value;
    cargarItems(filtro);
}

function crear() {
    const nombre = document.getElementById('nombre').value;
    const descripcion = document.getElementById('descripcion').value;
    fetch('https://localhost:44373/api/items/Save', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Nombre: nombre, Descripcion: descripcion })
    })
    .then(response => response.json())
    .then(data => {
        console.log('Item created:', data);
        cargarItems(); 
        document.getElementById('nombre').value = '';
        document.getElementById('descripcion').value = '';
    })
    .catch(error => console.error('Error:', error));
}

function eliminar(id) {
    fetch(`https://localhost:44373/api/items/Remove/${id}`, {
        method: 'DELETE'
    })
    .then(response => {
        if (response.ok) {
            cargarItems(); // cargar los items después de eliminar
        } else {
            alert('Error al eliminar el item');
        }
    })
    .catch(error => console.error('Error:', error));
}

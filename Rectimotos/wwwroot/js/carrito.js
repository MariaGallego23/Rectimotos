$(document).ready(function () {

    var carrito = [];


    function agregarAlCarrito(idProducto, nombre, precio) {
   
        var productoEnCarrito = carrito.find(function (p) {
            return p.idProducto === idProducto;
        });

        if (productoEnCarrito) {
          
            productoEnCarrito.cantidad++;
        } else {
      
            carrito.push({
                idProducto: idProducto,
                nombre: nombre,
                precio: precio,
                cantidad: 1
            });
        }

      
        actualizarModal();
    }

    function quitarDelCarrito(idProducto) {

        var productoEnCarrito = carrito.find(function (p) {
            return p.idProducto === idProducto;
        });

        if (productoEnCarrito) {
          
            productoEnCarrito.cantidad--;

 
            if (productoEnCarrito.cantidad === 0) {
                carrito = carrito.filter(function (p) {
                    return p.idProducto !== idProducto;
                });
            }
        }

   
        actualizarModal();
    }

  
    function actualizarModal() {
        var modalBody = $("#carritoModal .modal-body");
        var totalPagar = 0;

      
        modalBody.empty();

      
        carrito.forEach(function (producto) {
            modalBody.append("<p>" + producto.nombre + " - $" + producto.precio + " - Cantidad: " + producto.cantidad +
                " <button class='btn btn-danger btnQuitar' data-id='" + producto.idProducto + "'>Quitar</button></p>");
            totalPagar += producto.precio * producto.cantidad;
        });

       
        $("#total").text("Total: $" + totalPagar);
    }

   
    $(".btnAgregarCarrito").click(function () {
        var idProducto = $(this).attr("data-id");
        var nombreProducto = $(this).attr("data-nombre");
        var precioProducto = parseFloat($(this).attr("data-precio"));

        agregarAlCarrito(idProducto, nombreProducto, precioProducto);
    });

   
    $(document).on("click", ".btnQuitar", function () {
        var idProducto = $(this).attr("data-id");
        quitarDelCarrito(idProducto);
    });

    
    $("#vaciar-carrito").click(function () {
        carrito = [];
        actualizarModal();
    });

    

});
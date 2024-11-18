// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const menuButton = document.getElementById("menuButton");
const dropdown = document.getElementById("navbar-dropdown");

// Función para alternar la visibilidad del menú en pantallas pequeñas
menuButton.addEventListener("click", function () {
    dropdown.classList.toggle("hidden");  // Alterna la clase 'hidden' de Tailwind
    const isExpanded = dropdown.classList.contains("hidden");

    this.setAttribute("aria-expanded", !isExpanded);  // Actualiza el atributo aria-expanded
});

// Función para mostrar u ocultar el menú según el tamaño de la pantalla
function checkScreenSize() {
    if (window.innerWidth > 768) {
        dropdown.classList.remove("hidden"); // Mostrar el menú en pantallas grandes
        menuButton.setAttribute("aria-expanded", "true");
    } else {
        dropdown.classList.add("hidden"); // Ocultar el menú en pantallas pequeñas por defecto
        menuButton.setAttribute("aria-expanded", "false");
    }
}

// Verifica el tamaño de la pantalla cuando cambia la resolución
window.addEventListener("resize", checkScreenSize);

// Verifica el tamaño de la pantalla al cargar la página
checkScreenSize();

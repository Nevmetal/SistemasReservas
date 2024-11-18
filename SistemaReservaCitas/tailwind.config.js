/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./src/**/*.{html,js}",
        "./Views/**/*.cshtml", // Todas las vistas de Razor en tu proyecto ASP.NET
        "./Pages/**/*.cshtml", // Si usas Razor Pages
        './wwwroot/**/*.js',
        // Otras rutas donde pudieras tener HTML o scripts
    ],
  theme: {
    extend: {},
  },
  plugins: [],
}


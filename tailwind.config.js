/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Views/**/*.cshtml",
        "./Pages/**/*.cshtml",
        "./wwwroot/**/*.html"
    ],
    theme: {
        extend: {},
    },
    plugins: [require("daisyui")], // ✅ Ensure this is included
}

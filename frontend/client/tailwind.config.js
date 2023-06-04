/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        'quicksand': ['Quicksand', 'sans-serif'],
        'inter': ['Inter', 'sans-serif'],
      },
      colors: {
        current: 'currentColor',
      },
      fontSize: {
        '2xs': '.65rem',
      }
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
  ],
}
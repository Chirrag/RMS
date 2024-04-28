/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        'primary-blue':'#07539e',
        'secondary-blue': '#003056',
        'light-green': '#64AE3F',
        'dark-green': '#4F8931',
        'dark-gold': '#644314',
        'light-gold': '#C58427',
        'dark-purple': '#47133F',
        'light-purple': '#8A257A',
      }
    },
    fontFamily: {
      'eb': ['EB Garamond'],
      'op': ['Open Sans']
    },
    
  },
  plugins: [],
}

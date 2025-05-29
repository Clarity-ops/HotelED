module.exports = {
  purge: [],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      animation: {
        'fade-in': 'fade-in 0.3s ease-out forwards',
        'fade-in-up': 'fade-in-up 0.5s ease-out forwards'
      },
      keyframes: {
        'fade-in': {
          '0%': { 
            opacity: '0',
            transform: 'translateY(20px)'
          },
          '100%': { 
            opacity: '1',
            transform: 'translateY(0)'
          },
        },
          'fade-in-up': {
              '0%': {
                  opacity: '0',
                  transform: 'translateY(20px)'
              },
              '100%': {
                  opacity: '1',
                  transform: 'translateY(0)'
              }
          }
      }
  },
  variants: {
    extend: {},
  },
  plugins: [
    require('tailwindcss'),
    require('@tailwindcss/deprecation-warnings'),
    require('autoprefixer'),
  ],
  content: [
    "./Views/**/*.cshtml",
    "./Areas/**/Views/**/*.cshtml",
    "./**/*.html"
  ],
  }
}

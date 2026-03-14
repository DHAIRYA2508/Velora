/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      fontFamily: {
        sans: ['Cormorant Garamond', 'Georgia', 'serif'],
        display: ['Playfair Display', 'Georgia', 'serif'],
        body: ['Raleway', 'sans-serif'],
      },
      colors: {
        gold: {
          50:  '#fdfbf3',
          100: '#faf5e0',
          200: '#f4e8b0',
          300: '#ebd475',
          400: '#debb3d',
          500: '#c9a227',
          600: '#a8841c',
          700: '#856618',
          800: '#624c13',
          900: '#3d2f0c',
        },
        velora: {
          dark:    '#0a0a0a',
          charcoal:'#1a1a1a',
          warm:    '#2a2520',
          muted:   '#8b7355',
          cream:   '#f8f4ee',
          ivory:   '#fdfaf5',
        }
      },
      boxShadow: {
        'luxury': '0 25px 50px -12px rgba(0,0,0,0.4)',
        'gold': '0 4px 20px rgba(201,162,39,0.3)',
        'card': '0 8px 32px rgba(0,0,0,0.08)',
      },
      letterSpacing: {
        'widest2': '0.25em',
        'widest3': '0.35em',
      }
    },
  },
  plugins: [],
}

export const switchTheme = (theme) => {
  const root = document.documentElement
  let isDarkMode = theme === 'dark'

  if (theme === 'system') {
    localStorage.removeItem('color-theme')
    isDarkMode = window.matchMedia('(prefers-color-scheme: dark)').matches
  } else {
    localStorage.setItem('color-theme', theme)
  }

  root.classList.toggle('dark', isDarkMode)
}
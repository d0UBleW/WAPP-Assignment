$("[data-action='warn']").on('click', function () {
  return prompt('Please type in \"Yes, I am sure!\" to proceed') === 'Yes, I am sure!';
})

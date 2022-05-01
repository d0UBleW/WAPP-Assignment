//CKEDITOR.plugins.addExternal('codesnippet', '/ckeditor/plugins/codesnippet/')
//CKEDITOR.plugins.addExternal('embedsemantic', '/ckeditor/plugins/embedsemantic/')
CKEDITOR.replace('EditorTxtBox', {
    allowedContent: true,
    embed_provider: '//ckeditor.iframe.ly/api/oembed?url={url}&callback={callback}',
})

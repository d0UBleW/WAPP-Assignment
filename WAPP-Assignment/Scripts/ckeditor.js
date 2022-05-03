CKEDITOR.plugins.addExternal('codesnippet', '/ckeditor/plugins/codesnippet/')
CKEDITOR.plugins.addExternal('embedbase', '/ckeditor/plugins/embedbase/')
CKEDITOR.plugins.addExternal('autolink', '/ckeditor/plugins/autolink/')
CKEDITOR.plugins.addExternal('autoembed', '/ckeditor/plugins/autoembed/')
CKEDITOR.plugins.addExternal('textmatch', '/ckeditor/plugins/textmatch/')
CKEDITOR.plugins.addExternal('embedsemantic', '/ckeditor/plugins/embedsemantic/')
CKEDITOR.replace('EditorTxtBox', {
    extraPlugins: 'codesnippet,embedsemantic,autoembed',
    embed_provider: '//ckeditor.iframe.ly/api/oembed?url={url}&callback={callback}',
})

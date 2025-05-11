docsCategory = db.categories.findOne({ "name.be": "Дакументы" })

oldEastSlavic = db.languages.findOne({ "name.be": "Старажытнаруская" })
ruthenian = db.languages.findOne({ "name.be": "Старабеларуская" })  

db.historicalDocs.insertOne({
    "title": {
        "be": "Судзебнік Казіміра 1468 г.",
        "ru": "Судебник Казимира 1468 г."
    },
    "creationTime": {
        "be": "1468 г.",
        "ru": "1468 г."
    },
    "author": {
        "be": "Казімір IV Ягелончык",
        "ru": "Казимир IV Ягеллончик"
    },
    "languageId": ruthenian._id,
    "categoryId": docsCategory._id,
    "subCategoryId": db.categories.findOne({ "name.be": "Права", "parentId": docsCategory._id })._id,
    "fileId": db.fs.files.findOne({ filename: "D:\\Downloads\\Судзебнік_Казіміра_1468_г.pdf" })._id,
    "tags": [
        "TestTag0", "TestTag1"
    ]
})
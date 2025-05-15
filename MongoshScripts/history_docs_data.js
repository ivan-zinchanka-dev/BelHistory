const docsCategory = db.categories.findOne({ "name.be": "Дакументы" })

const oldEastSlavic = db.languages.findOne({ "name.be": "Старажытнаруская" })
const ruthenian = db.languages.findOne({ "name.be": "Старабеларуская" })
const russian = db.languages.findOne({ "name.be": "Руская" })

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
    "fileId": db.fs.files.findOne({ filename: /Судзебнік_Казіміра_1468_г\.pdf$/i })._id,
    "tags": [
        "TestTag0", "TestTag1"
    ]
})

db.historicalDocs.insertOne({
    "title": {
        "be": "Прывілей Казіміра 1447 г.",
        "ru": "Привелей Казимира 1447 г."
    },
    "creationTime": {
        "be": "1447 г.",
        "ru": "1447 г."
    },
    "author": {
        "be": "Казімір IV Ягелончык",
        "ru": "Казимир IV Ягеллончик"
    },
    "languageId": ruthenian._id,
    "categoryId": docsCategory._id,
    "subCategoryId": db.categories.findOne({ "name.be": "Права", "parentId": docsCategory._id })._id,
    "fileId": db.fs.files.findOne({ filename: /Прывілей_Казіміра_1447_г\.pdf$/i })._id,
    "tags": [
        "TestTag0", "TestTag1"
    ]
})

db.historicalDocs.insertOne({
    "title": {
        "be": "Прывілей Аляксандра 1492 г.",
        "ru": "Привелей Александра 1492 г."
    },
    "creationTime": {
        "be": "1492 г.",
        "ru": "1492 г."
    },
    "author": {
        "be": "Аляксандр IV Ягелончык",
        "ru": "Александр IV Ягеллончик"
    },
    "languageId": ruthenian._id,
    "categoryId": docsCategory._id,
    "subCategoryId": db.categories.findOne({ "name.be": "Права", "parentId": docsCategory._id })._id,
    "fileId": db.fs.files.findOne({ filename: /Прывілей_Аляксандра_1492_г\.pdf$/i })._id,
    "tags": [
        "TestTag0", "TestTag1"
    ]
})

db.historicalDocs.insertOne({
    "title": {
        "be": "Судзебнік Івана 1497 г. (арыгінал)",
        "ru": "Судебник Ивана 1497 г. (оригинал)"
    },
    "creationTime": {
        "be": "1497 г.",
        "ru": "1497 г."
    },
    "author": {
        "be": "Іван III Васільевіч",
        "ru": "Иван III Васильевич"
    },
    "languageId": oldEastSlavic._id,
    "categoryId": docsCategory._id,
    "subCategoryId": db.categories.findOne({ "name.be": "Права", "parentId": docsCategory._id })._id,
    "fileId": db.fs.files.findOne({ filename: /Судебник_Ивана_1497_г\._оригинал\.pdf$/i })._id,
    "tags": [
        "TestTag0", "TestTag1"
    ]
})

db.historicalDocs.insertOne({
    "title": {
        "be": "Судзебнік Івана 1497 г. (новая рэдакцыя)",
        "ru": "Судебник Ивана 1497 г. (новая редакция)"
    },
    "creationTime": {
        "be": "1497 г.",
        "ru": "1497 г."
    },
    "author": {
        "be": "Іван III Васільевіч",
        "ru": "Иван III Васильевич"
    },
    "languageId": russian._id,
    "categoryId": docsCategory._id,
    "subCategoryId": db.categories.findOne({ "name.be": "Права", "parentId": docsCategory._id })._id,
    "fileId": db.fs.files.findOne({ filename: /Судебник_Ивана_1497_г\._новая_редакция\.pdf$/i })._id,
    "tags": [
        "TestTag0", "TestTag1"
    ]
})
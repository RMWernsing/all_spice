export class Recipe {
  constructor(data) {
    this.id = data.id
    this.category = data.category
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.creatorId = data.creatorId
    this.img = data.img
    this.instructions = data.instructions
    this.title = data.title
  }
}
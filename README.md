# Koumakan Model Exploder

用于自动从带层级结构的3D模型构建爆炸图的目标位置，并形成爆炸图动画。

## 用法

创建一个 GameObject，挂载 `Exploder.cs` 脚本，然后将要生成爆炸图的模型 Transform 组件拖到 Target
属性上，调整参数后，可以点击 **Calculate Target Layout** 进行布局计算，单击 **Explode!** 可以进行爆炸，**Undo** 可以撤销。

需要注意的是，建议在 Editor 模式中**不要直接使用 Explode!**，因为这会直接修改场景中的游戏对象的相对位置关系，且**无法撤销**，建议在 Play 模式下测试。

你可以在 `Sample/AxisLayoutExample.unity` 场景中查看示例。
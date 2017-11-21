|参数名|用途
|:----|:----
|Interactable | 是否允许交互，反勾选的华InputField就失活了。
|Transition| InputField的状态机制，和Button中的用法相同，不再细讲。
|Navigation| 导航功能，和Button中的用法相同，不再细讲。
|Text Component|  绑定一个Text组件，来呈现输入后的东西，被绑定于此处的物体，不能在Inspector中修改参数（默认绑定了子物体Text）。
|Text|  当前输入框里已输入的文字。
|Character Limit| 输入字体的最大字号，默认为0，即自适应大小。
|Content Type | 输入内容的类型：
|Standard|  标准，即不限制
|AuroCorrected|  自动校正
|Integer Number|  只允许输入整数
|Decimal Number | 只允许输入带一个小数点的十进制数
|Alphanumeric | 允许字母和数字，不允许符号
|Name | 输入单次的第一个字母自动大写
|Email Address|  允许输入带有@符号的字符串。
|Password | 用 * 号隐藏已经输入的字符串。
|Pin|  只允许输入整数，用 * 号隐藏已经输入的内容。
|Custom|  传统自定义输入格式。
|Line Type|  定义了输入框里的行数：
|Single Line| 只允许输入一行
|MultiLine Submit | 允许输入多行，但只有最后一行会被采用。
|MultiLine NewLine|  允许输入多行，按回车键将会另起一行。
|Placeholder | 绑定一个Text物体，当输入框未输入内容时、将会默认呈现Placeholder中的内容。
|Selection Color|  已输入的内容被选中时的颜色。
|Read Only | 只读
|Hide Mobile Input|  隐藏手机屏幕键盘本身的输入框（仅针对iOS手机）
|On Value Changed（） | 当输入框内容改变时触发事件系统
|On Submit（） | 当光标移开输入框或敲击回车时触发事件系统

﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//行为(behavior)是一款重用用户界面代码的更有挑战性的工具。其基本思想是：使用行为封装一些通用的UI功能(例如，使元素可被拖动的代码)。如果具有适当的行为，可使用一两行XAML标记
//将其附加到任意元素，从而为您节省编写和调试代码的工作

//行为旨在封装一些UI功能，从而可以不必编写代码就能够将其应用到元素上。从另一个角度看，每个行为都为元素提供了一个服务。该服务通常涉及监听几个不同的事件并执行几个相关的操作
//(例如，为文本框提供了水印行为。如果文本框为空，并且当前没有焦点，那么会以轻淡的字体显示提示信息(如"[Enter text here]")。当文本框具有焦点时，启动行为并删除水印文本)

//样式提供了重用一组属性设置的实用方法。但还有许多限制。问题是在典型的应用程序中，属性设置仅是用户界面基础结构的一小部分。甚至最基本的程序通常也需要大量的用户界面代码，这些代码
//与应用程序的功能无关。在许多程序中，用于用户界面任务的代码(如驱动动画、实现平滑效果、维护用户界面状态，以及支持诸如拖放、缩放以及停靠等用户界面特性)无论是在数量上还是复杂性上都
//超出了业务代码。许多这类代码是通用的，这意味着在创建的每个WPF对象中需要编写相同的内容。所有这些工作几乎都是单调乏味的

//为回应这一挑战，Expression Blend创作者开发了称为行为(behavior)的特征。其思想很简单：
//您(或其他开发人员)创建封装了一些通用用户界面功能的行为。这一功能可以是基本功能(如启动故事板或导航到超链接)，也可以是复杂功能(如处理多点触摸交互，或构建使用实时物理引擎的碰撞模型)。
//一旦构建功能，就可将他们添加到任意应用程序的另一个控件中，具体方法是将该控件链接到适当的行为并设置行为的属性。在Expression Blend中，只通过拖放操作就可以使用行为

//注意，自定义控件是另一个在一个应用程序中(或在多个应用程序之间)重用用户界面功能的技术。然而，自定义控件必须作为可视化内容和代码的紧密链接包进行创建。尽管自定义控件非常强大，但
//却不能适应于需要大量具有类似功能的不同控件的情况(例如，为一组不同的元素添加鼠标悬停渲染效果)。因此，样式、行为以及自定义控件都是互补的




//获取行为支持

//重用用户界面代码通用块的基础结构不是WPF的一部分，他被捆绑到Expression Blend，这是因为行为开始是作为Expresion Blend的设计时特性引入的。事实上，Expression Blend仍是通过将行为
//拖动到需要行为的控件上来添加行为的唯一工具。但这并不意味着行为只能用于Expression Blend。只需要付出很少的努力就可以在visual studio应用程序中创建和使用行为。只需要手动编写标记，而不是
//使用工具箱

//两个重要程序集：
//1.System.Windows.Interactivity.dll
//这个程序集定义了支持行为的基本类。他是行为特征的基础
//2.Microsoft.Expression.Interactions.dll
//这个程序集通过添加可选的以核心行为类为基础的动作和触发器类，增加了一些有用的扩展






//理解行为模型

//行为特性具有两个版本:
//一个版本旨在为Silverlight添加行为支持
//另一个版本是针对WPF设计的

//尽管这两个版本提供了相同的特性，但行为特性和silverlight领域更吻合，因为他弥补了更大的鸿沟，与WPF不同，Silverlight不支持触发器，所以实现行为的程序集也实现触发器更合理。然而，WPF
//支持触发器，行为特性包含自己的触发器系统，而触发器系统与WPF模型不匹配，这确实令人感到有些困惑

//问题在于具有类似名称的这两个特性有部分重合但不完全相同。在WPF中，触发器最重要的角色是构建灵活的样式和控件模板。在触发器的帮助下，样式和模板变得更加智能；例如，当一些属性发生变化时
//可应用可视化效果。然而，Expression Blend中的触发器系统具有不同的目的。通过使用可视化设计工具，允许您为应用程序添加简单功能。换句话说，WPF触发器支持更加强大的样式和控件模板。而
//Expression Blend触发器支持快速的不需要代码的应用程序设计

//那么，对于使用WPF的普通开发人员来说所有这些意味着什么呢？下面时几条指导原则：
//1.行为模型不是WPF的核心部分，所以行为不像样式和模板那样确定。换句话说，可编写不使用行为的WPF应用程序，但如果不使用样式和模板，就不能创建比“Hello World”演示更复杂的WPF应用程序
//2.如果在Expression Blend上耗费大量时间，或希望为其他Expression Blend用户开发组件，您可能会对Expression Blend中的触发器特性感兴趣。尽管和WPF中的触发器系统使用相同的名称，但他们
//不相互重叠，您可以同时使用这两者
//3.如果不使用Expression Blend，可完全略过其触发器特性——但仍应分析Expression Blend提供的功能完整的行为类。这是因为行为比Expression Blend的触发器更强大也更常用。最终，您将准备查找
//那些提供了可在您自己的应用程序中使用的整洁美观行为的第三方组件




namespace WPF_Behavior
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
    }
}
